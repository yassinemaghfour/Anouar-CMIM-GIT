import React from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import Paper from '@material-ui/core/Paper';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import SvgIcon from '@material-ui/core/SvgIcon';
import Divider from '@material-ui/core/Divider';
import IconButton from '@material-ui/core/IconButton';
import axios from 'axios';
import jsPDF from 'jspdf';
import Dossies from '../Dossies/Dossies';
import 'jspdf-autotable';
import Info from '@material-ui/icons/Info';
import { Tooltip } from '@material-ui/core';
import withAuth from '../withAuth.js';
import AuthService from "../AuthHelperMethods";
import {CSVLink, CSVDownload} from 'react-csv';

const styles = theme => ({
  root: {
    flexGrow: 1,
  },
  paper: {
    padding: theme.spacing.unit * 2,
    textAlign: 'left',
    color: theme.palette.text.secondary,
    background: "blue"
  },

  divider: {
    margin: `${theme.spacing.unit * 2}px 0`,
  },
  textField: {
    marginLeft: theme.spacing.unit,
    marginRight: theme.spacing.unit,
  },
  dense: {
    marginTop: 16,
  },
  menu: {
    width: 200,
  }
});

class FullWidthGrid extends React.Component  {
  constructor(){
    super();
    this.Auth = new AuthService();
}
  handleChange = name => event => {
    this.setState({
      [name]: event.target.value,
    });
  };
  state = {
    svl:'',
    veas:'',
    vem:'',
    attenteEnvoi: '',
    envoyee: '',
    remboursee: '',
    bordereauId: '',
    data:[]
  }
  
  componentDidMount()
  {
      console.log("Profile:");
      console.log(this.Auth.getProfile());
    // axios.get('/api/Users')
    // .then(res => {
    //   if(res.status == 200) {
    //     console.log("Users:" );
    //     console.log(res);
    //   }
    //   else
    //   console.log("Error");
    // });

    axios.get('/api/Dashboard')
    .then(res => {
      console.log("load data");
      console.log(res);
      if(res.status == 200)
      {
        for(var i = 0; i < 3; i++) {
          if(res.data[i] != null) {
          if(res.data[i].company == "VEM") {
            this.setState({
              vem:(res.data[i] == undefined ? 0 : res.data[i].count )
            })
        }
        else if(res.data[i].company == "VEAS") {
          this.setState({
            veas:(res.data[i] == undefined ? 0 : res.data[i].count )
          })
        }
        else {
          this.setState({
            svl:(res.data[i] == undefined ? 0 : res.data[i].count )
          })
        }
      }
    }
      }
    });
    axios.get('/api/Statistique')
    .then(res => {
      console.log("Statistique data");
      console.log(res);
      if(res.status == 200)
      {
        for(var i = 0; i < res.data.length; i++) {
          if(res.data[i] != null) {
            console.log(res.data[i].etat + " : " + res.data[i].count);
          if(res.data[i].etat == "Avancé") {
            this.setState({
              attenteEnvoi:(res.data[i].count == undefined ? 0 : res.data[i].count )
            })
        }
        else if(res.data[i].etat == "Envoyé à la CMIM") {
          this.setState({
            envoyee:(res.data[i].count == undefined ? 0 : res.data[i].count )
          })
        }
        else if(res.data[i].etat == "Remboursé") {
          this.setState({
            remboursee:(res.data[i].count == undefined ? 0 : res.data[i].count )
          })
        }
      }
    }
      }
    });
  }
  handlePrintClick = company =>
  {
    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = today.getFullYear();
    var hh = String(today.getHours()).padStart(2, '0');
    var MMM = String(today.getMinutes()).padStart(2, '0');

  today = mm + '/' + dd + '/' + yyyy + ' ' + hh + ':' + MMM;
    const data = {
      'Company' : company,
      'DateCreation': today
  };

    axios.post('/api/Bordereaux', data)
      .then(res => {
        if (res.status == 201)
        {
          this.setState({
            open:true,
            message:"Ajouter avec succes",
            state: this.state,
            bordereauId: res.data.bordereauId
          });

          axios.get('/api/Dashboard/'+this.state.bordereauId)
          .then(res=> {
            if (res.status == 200)
            {
              console.log(res);
              this.setState({
                data:res.data
              });
              console.log("datas: ");
              console.log(data["Company"]);
            }
            var tabs = [];
           console.log("jsPDF Data: ")
           console.log(this.state.data);
           for(var i = 0; i < this.state.data.length; i++) {
             tabs[i] = [];
             tabs[i][0] = this.state.data[i]["referance"];
             tabs[i][1] = this.state.data[i]["employeematricule"];
             tabs[i][2] = this.state.data[i]["employee"]["buId"] ;
             tabs[i][3] = this.state.data[i]["employee"]["last_name"] + " " + this.state.data[i]["employee"]["first_name"];
             tabs[i][4] = this.state.data[i]["montant"];
             tabs[i][5] = this.state.data[i]["avance"];
             
      
           }
           console.log("jsPDF: ")
           console.log(tabs);
           const pdf = new jsPDF('p', 'pt', 'letter');
           pdf.text(220, 30, "Bordereau Numéro: " + this.state.bordereauId);
           pdf.autoTable({
                  head:[["Numero dossier CMIM", "Matricule employé", "Numéro BU", "Nom et Prénom", "Montant Dossier", "Montant Avance", ]],
                   body:tabs
                       });

           pdf.save("borderaux_Numero_" + this.state.bordereauId + ".pdf");
           
          });
        }

        if(data["Company"] == "vem") {
          //max
          if(this.state.vem <= 25)
              this.state.vem = 0;
          else
              this.state.vem -= 25
       }
       else if(data["Company"]  == "veas") {
         //max
         if(this.state.veas <= 25)
         this.state.veas = 0;
     else
         this.state.veas -= 25
       }
       else {
         //max
         if(this.state.svl <= 25)
         this.state.svl = 0;
     else
         this.state.svl -= 25
       }
      
      });
  }
  formatDate = (date) => {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = '' + d.getFullYear(),
        minutes = '' + d.getMinutes(),
        hours = '' + d.getHours();

    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;

    return [day, month, year].join('-') + ' ' + [hours, minutes].join[':'];
 };
  render() {
    const max = 25;
    const { classes } = this.props;
  return (

    <div className={classes.root}>
    <Grid container spacing={24} >
      <Grid item >
        <SvgIcon style={{ fontSize: 30,marginLeft: '-3px'}}><path d="M19 3H5c-1.1 0-2 .9-2 2v14c0 1.1.9 2 2 2h14c1.1 0 2-.9 2-2V5c0-1.1-.9-2-2-2zM9 17H7v-7h2v7zm4 0h-2V7h2v10zm4 0h-2v-4h2v4z"/><path d="M0 0h24v24H0z" fill="none"/></SvgIcon>
      </Grid>
      <Grid item >
        <Typography component="h2" variant="button" gutterBottom style={{lineHeight: 1.5,fontSize: '22px'}}>
          Tableau de bord
        </Typography>
      </Grid>
    </Grid>
      <Grid container spacing={24} >
        <Grid item xs={12} sm={4} >
          <Paper  className={classes.paper} style={{backgroundColor:"#A7A9AC"}}>
            <Typography  variant="button" gutterBottom  style={{color:"#fff"}}>
                VEM (nouveaux dossiers saisis)
                { (this.state.vem != '' && this.state.vem >= 1) ? 
                <Tooltip title="Créer un bordereau de 25 dossiers">
            <IconButton className={classes.button} style={{color:"#fff"}}  onClick={()=>{this.handlePrintClick('vem')}}>
            <SvgIcon>
            <path fill="none"  d="M0 0h24v24H0V0z"/><path d="M20 12l-1.41-1.41L13 16.17V4h-2v12.17l-5.58-5.59L4 12l8 8 8-8z"/>
              </SvgIcon> </IconButton></Tooltip>
              : 
              <IconButton className={classes.button} style={{color:"#fff"}} ><SvgIcon><Info/></SvgIcon></IconButton>
              }
            </Typography>

            <Typography  variant="h3" gutterBottom  style={{color:"#fff"}}>
              {this.state.vem == '' ? 0 : this.state.vem} / {max}
            </Typography>
          </Paper>
        </Grid>
        <Grid item xs={12} sm={4} >
          <Paper className={classes.paper} style={{backgroundColor:"#A7A9AC"}}>
            <Typography  variant="button" gutterBottom style={{color:"#fff"}}>
              VEAS (nouveaux dossiers saisis)
              { (this.state.veas != '' && this.state.veas >= 1) ? 
              <Tooltip title="Créer un bordereau de 25 dossiers">
            <IconButton className={classes.button} style={{color:"#fff"}}  onClick={()=>{this.handlePrintClick('veas')}}>
            <SvgIcon>
            <path fill="none"  d="M0 0h24v24H0V0z"/><path d="M20 12l-1.41-1.41L13 16.17V4h-2v12.17l-5.58-5.59L4 12l8 8 8-8z"/>
              </SvgIcon> </IconButton></Tooltip>
              : 
              <IconButton className={classes.button} style={{color:"#fff"}} ><SvgIcon><Info/></SvgIcon></IconButton>
              }
            </Typography>
            <Typography  variant="h3" gutterBottom  style={{color:"#fff"}}>
            {this.state.veas == '' ? 0 : this.state.veas} / {max}
            </Typography>
          </Paper>
        </Grid>
        <Grid item xs={12} sm={4} >
        <Paper className={classes.paper} style={{backgroundColor:"#A7A9AC"}}>
            
            <Typography  variant="button" gutterBottom style={{color:"#fff"}} >
              SVL (nouveaux dossiers saisis)
            { (this.state.svl != '' && this.state.svl >= 1) ? 
            <Tooltip title="Créer un bordereau de 25 dossiers">
            <IconButton className={classes.button} style={{color:"#fff"}}  onClick={()=>{this.handlePrintClick('svl')}}>
            <SvgIcon>
                <path fill="none"  d="M0 0h24v24H0V0z"/><path d="M20 12l-1.41-1.41L13 16.17V4h-2v12.17l-5.58-5.59L4 12l8 8 8-8z"/>
              </SvgIcon> </IconButton></Tooltip>
              : 
              <IconButton className={classes.button} style={{color:"#fff"}} ><SvgIcon><Info/></SvgIcon></IconButton>
              }
           
            </Typography> 
            <Typography  variant="h3" gutterBottom  style={{color:"#fff"}}>
            {this.state.svl == '' ? 0 : this.state.svl} / {max}
            </Typography>
          </Paper>
        </Grid>
        <Grid item xs={12} sm={4} >
        <Paper className={classes.paper} style={{backgroundColor:"#A7A9AC"}}>
            
            <Typography  variant="button" gutterBottom style={{color:"#fff"}} >
              Les dossiers en attente d'envoi 
              <br/>à la CMIM
              <IconButton className={classes.button} style={{color:"#fff"}} >
              <SvgIcon><Info/></SvgIcon>
              </IconButton>
              
           
            </Typography> 
            <Typography  variant="h3" gutterBottom  style={{color:"#fff"}}>
            {this.state.attenteEnvoi == '' ? 0 : this.state.attenteEnvoi}
            </Typography>
          </Paper>
        </Grid>
        <Grid item xs={12} sm={4} >
        <Paper className={classes.paper} style={{backgroundColor:"#A7A9AC"}}>
            
            <Typography  variant="button" gutterBottom style={{color:"#fff"}} >
            Les dossiers en attente d'envoi 
             à la CMIM et non remboursés
              <IconButton className={classes.button} style={{color:"#fff"}} >
              <SvgIcon><Info/></SvgIcon>
              </IconButton>
              
           
            </Typography> 
            <Typography  variant="h3" gutterBottom  style={{color:"#fff"}}>
            {this.state.envoyee == '' ? 0 : this.state.envoyee}
            </Typography>
          </Paper>
        </Grid>
        <Grid item xs={12} sm={4} >
        <Paper className={classes.paper} style={{backgroundColor:"#A7A9AC"}}>
            
            <Typography  variant="button" gutterBottom style={{color:"#fff"}} >
              Les dossiers remboursés par la CMIM et non régularisés
              <IconButton className={classes.button} style={{color:"#fff"}} >
              <SvgIcon><Info/></SvgIcon>
              </IconButton>
              
           
            </Typography> 
            <Typography  variant="h3" gutterBottom  style={{color:"#fff"}}>
            {this.state.remboursee == '' ? 0 : this.state.remboursee}
            </Typography>
          </Paper>
        </Grid>
      </Grid>
    
     
      <Divider className={classes.divider} />
      <Dossies/>
    </div>
  )};
}
FullWidthGrid.propTypes = {
  classes: PropTypes.object.isRequired,
};
export default withAuth(withStyles(styles)(FullWidthGrid));
