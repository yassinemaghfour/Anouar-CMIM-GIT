import React from 'react';
import PropTypes from 'prop-types';
import classNames from 'classnames';
import { withStyles } from '@material-ui/core/styles';
import MenuItem from '@material-ui/core/MenuItem';
import TextField from '@material-ui/core/TextField';
import Paper from '@material-ui/core/Paper';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import AddIcon from '@material-ui/icons/Add';
import IconButton from '@material-ui/core/IconButton';
import Tooltip from '@material-ui/core/Tooltip';
import Button from '@material-ui/core/Button';
import axios from 'axios';
import jsPDF from 'jspdf';
import LinearProgress from '@material-ui/core/LinearProgress';
import DeleteIcon from '@material-ui/icons/Delete';
import EditIcon from '@material-ui/icons/Edit';
import Snackbar from '@material-ui/core/Snackbar';
import CloseIcon from '@material-ui/icons/Close';
import Regularisationemployee from './Regularisationemployee'
import withAuth from '../withAuth.js';
import { CSVLink, CSVDownload } from "react-csv";
import { confirmAlert } from 'react-confirm-alert'; // Import
import 'react-confirm-alert/src/react-confirm-alert.css'; 

const styles = theme => ({
  container: {
    display: 'flex',
    flexWrap: 'wrap',
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
  },
  paper: {
    padding: theme.spacing.unit * 2,
    textAlign: 'center',
    color: theme.palette.text.secondary,
  },
  table: {
    minWidth: 700,
  },
  paper: {
    position: 'absolute',
    width: theme.spacing.unit * 50,
    backgroundColor: theme.palette.background.paper,
    boxShadow: theme.shadows[5],
    padding: theme.spacing.unit * 4,
  },
});
;

class RegularisationForm extends React.Component {
  state = {
    multiline: 'Controlled',
    open:false,
    regularisationID:'',
    date: new Date(),
    montant:'',
    avance:'',
    rembourse:'',
    diff:'',
    etat:'',
    employee:'',
    add:false,
    update:false,
    isLoad:false,
    isClickable:true,
    tabs: [],
    isDownloadable: false,
    open:true,
    message:'',
    selectedDate: new Date('2014-08-18T21:11:54'),
    isAdd : false,
    regularisation_employee:[],
  };
 formatDate = (date) => {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;

    return [year, month, day].join('-');
}
  handleDateChange = date => {
      this.setState({ selectedDate: date });
    }
  handleChange = name => event => {
    this.setState({
      [name]: event.target.value,
    });
  };
  handleOpen = () => {
    this.setState({ open: true });
  };

  handleClose = () => {
    this.setState({ open: false });
  };
  handleUpdateClick = () =>
  {
    this.setState({
      update:true
    })
  }
  handleDeleteClick = () =>
  {
   /* axios.delete('/api/Dossiers/'+this.state.regularisationID)
      .then(res => {
        console.log(res);
        if(res.status == 200)
        {
          this.props.history.push("/dossiers");
        }
      })*/
  }
  handleCancelClick = () => {

  }
handleSubmit = (e) => {
  this.setState({
    isLoad:true
  });
  console.log("Mois: " + e.target.mois.value);
  var today = new Date();
var dd = String(today.getDate()).padStart(2, '0');
var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
var yyyy = today.getFullYear();
var hh = String(today.getHours()).padStart(2, '0');
var MMM = String(today.getMinutes()).padStart(2, '0');

today = mm + '/' + dd + '/' + yyyy + ' ' + hh + ':' + MMM;
  const data = {
    'Mois':e.target.mois.value,
    'dateRegularisation': today
  };
  console.log(data);
  if(!this.state.update && this.state.add)
  {
     axios.post('/api/Regularisation',data)
      .then(res => {
        if (res.status == 201)
        {

          console.log("regul data");
          console.log(res);

          var tabs = [];
          console.log("jsPDF Data: ");
          if(res.data != null) {
            this.setState({
              open:true,
              message:"La régularisation du mois: " + data.Mois + " a bien été effectué, contenant " + res.data.length + " dossier(s)",
              regularisationID:res.data[0]["regularisationId"]
            });
          console.log(res.data);
     for(var i = 0; i < res.data.length; i++) {
       tabs[i] = [];
       tabs[i][0] = res.data[i]["empMatricule"];
       tabs[i][1] = res.data[i]["matriculeCmim"];
       tabs[i][2] = res.data[i]["numDo"];
       tabs[i][3] = res.data[i]["numBu"];
       tabs[i][4] = res.data[i]["nom"];
       tabs[i][5] = res.data[i]["avance"];
       tabs[i][6] = res.data[i]["remboursee"];
       tabs[i][7] = res.data[i]["regul"];
     }

     var tabHeader = ["Matricule employée", "Matricule CMIM", "Dossier N°", "Bordereau N°", "Nom employée", "Avance", "Remboursée", "Montant de régularisation"];
     
     tabs.unshift(tabHeader);
     console.log("jsPDF: ")
     console.log(tabs);
     this.setState({
       isDownloadable : true,
       tabs : tabs
     })
    /* const pdf = new jsPDF('p', 'pt', 'letter'); */
   /*  var splitTitle = pdf.splitTextToSize("Impression de bordereau", 180);
     pdf.text(30, 20, splitTitle);
     var splitTitle = pdf.splitTextToSize("Date Impression: " , 180);
     pdf.text(30, 50, splitTitle);*/
     /*pdf.text(30, 30, "Régularisation Numéro: " + this.state.regularisationID + " Pour le mois de: " + res.data.mois);
     pdf.autoTable({
            head:[["Matricule Employé", "Matricule CMIM", "Numéro dossiers CMIM" ,"Numéro BU", "Nom et Pénom", "Mt avance", "Mt Rembourssé", "Mt Régularisation"]],
             body:tabs
                 });
     console.log(document.getElementById('printedTable'));
     console.log('ffffffffffffffffff');
     console.log(this.refs.printedTable);
     // const htmlTable = ReactDOMServer.renderToString(this.refs.printedTable)
     // console.log(htmlTable);
     pdf.save("Liste_Des_Employées_Réglés_Numéro_" + this.state.regularisationID + ".pdf");*/

     /*confirmAlert({
      title: 'Message de confirmation',
      message: 'Voulez-vous confirmer l\'envoi à la CMIM?',
      buttons: [
        {
          label: 'Imprimer le fichier',
          onClick: () => {
            
          }
        },
      ]
    });*/

        }
        else {
          this.setState({
            open:true,
            message:"Régularisation généré avec 0 dossier",
            regularisationID:res.data[0]["regularisationId"]
          });
        }
      }
      });
    
  }

    this.setState({
      isLoad:false,
      add:false,
      update:false
    });

  e.preventDefault();
  }
  componentDidMount()
  {
    this.setState({
      date: new Date()
    });
    console.log("is Add:");
    console.log(this.props.isAdd);
    if (this.props.isAdd == "True")
    {
      this.setState({
        isLoad:false
      });
      return;
    }

      console.log('bbbbbbbbbbbbbbbbbbbb');
      this.setState({
        regularisationID:this.props.match.params.regulID
      });

    axios.get(`/api/Regularisation/${this.props.match.params.regulID}`)
    .then(res => {
      if(res.status == 200)
      {
        console.log(res);
        this.setState({
          date:new Date(res.data.dateRegularisation),
          mois: res.data.mois,
          regularisation_employee: res.data["listDesRégularisation"],
          isLoad: true
        });
        console.log("datas: ");
        console.log(this.state);
      }
  });
    
    this.setState({
      isLoad:false
    })
  }

  constructor(props)
  {
     super(props);
     console.log("test");
     console.log(props);
     this.state = {
    regularisation_employee:[],
    tabs: [],
    isDownloadable: false,
       regularisationID:props.isAdd == "True" ? '' : props.match.params.regulID,
       etat:'ouvert',
       add:props.isAdd == "True" ? true : false,
       isLoad:props.isAdd == "true" ? false : true ,
     }
  }
  render() {
    const { classes } = this.props;

    return (

      <Grid container style={{width:"90%",margin:"0 auto"}}>
        <Grid item xs={12}  >
          <Paper>
          {!this.state.isLoad && <LinearProgress color="primary" />}

          <form className={classes.container} Validate autoComplete="off" style={{textAlign:"center"}} onSubmit={this.handleSubmit} >

          <Grid container xs={6} >
              <Typography variant="h5" gutterBottom style={{padding:'20px'}} >
                {this.state.regularisationID != '' ? "Régularisation N°: " + this.state.regularisationID : 'Nouveau Régularisation'}
              </Typography>
    {this.state.isDownloadable && <CSVDownload data={this.state.tabs} target="_blank" />};
          </Grid>
          <Grid container xs={6} style={{justifyContent:'flex-end',padding:'18px'}}>
          {(this.state.add || this.state.update) &&
            <div>
            {!this.state.isLoad &&
              <Button variant="contained"   type="submit" color="primary" className={classes.button}>
                    Génerer le fichier de régularisation
              </Button>
            }

              	&nbsp;	&nbsp;
              {!this.state.isLoad &&
                <Button className={classes.button} onClick={this.handleCancelClick}>
                      annuler
                </Button>
              }

            </div>

          }
          {(!this.state.add && !this.state.update) &&
            (
                <Tooltip title="Supprimer" aria-label="Supprimer">
                  <IconButton  className={classes.button} component="span" onClick={this.handleDeleteClick} >
                    <DeleteIcon/>
                  </IconButton>
                </Tooltip>

            )
          }
          </Grid>
            <Grid container  xs={12}  >
            {this.state.referance == '' &&
              <Grid item xs={4} style={{padding:"0 30px"}}>

                <TextField
                  id="regularisationID"
                  label="ID Régularisation"
                  margin="normal"
                  className={classes.textField}

                  helperText="Obligatoire"
                  style={{marginBottom:"20px",width:"100%"}}
                  InputProps={{
                    readOnly: !(this.state.add || this.state.update),
                  }}
                />
              </Grid>
              }
              <Grid item xs={4} style={{padding:"0 30px"}}>
                  <TextField
                    id="mois"
                    select
                    label="Mois de régularisation"
                    margin="normal"
                    className={classes.textField}
                    value={this.state.mois}
                    onChange={this.handleChange('mois')}
                    SelectProps={{
                      MenuProps: {
                        className: classes.menu,
                      },
                    }}
                    helperText="Obligatoire"
                    margin="normal"
                    style={{width:"200px",width:"100%"}}
                    InputProps={{
                      readOnly: !(this.state.add || this.state.update)
                    }}
                    InputLabelProps={{
                      shrink: true,
                    }}
                    >
                    <MenuItem key="Janvier" value="Janvier">Janvier</MenuItem>
                    <MenuItem key="Février" value="Février">Février</MenuItem>
                    <MenuItem key="Mars" value="Mars">Mars</MenuItem>
                    <MenuItem key="Avril" value="Avril">Avril</MenuItem>
                    <MenuItem key="Mai" value="Mai">Mai</MenuItem>
                    <MenuItem key="Juin" value="Juin">Juin</MenuItem>
                    <MenuItem key="Juillet" value="Juillet">Juillet</MenuItem>
                    <MenuItem key="Août" value="Août">Août</MenuItem>
                    <MenuItem key="Septembre" value="Septembre">Septembre</MenuItem>
                    <MenuItem key="Octobre" value="Octobre">Octobre</MenuItem>
                    <MenuItem key="Novembre" value="Novembre">Novembre</MenuItem>
                    <MenuItem key="Décembre" value="Décembre">Décembre</MenuItem>
                   </TextField>
              </Grid>
              <Grid item xs={4} style={{padding:"0 30px"}}>
                <TextField
                  id="date"
                  label="Date de régularisation"
                  value={this.formatDate(this.state.date)}
                  type="date"
                  className={classes.textField}
                  onChange={this.handleChange('date')}
                  margin="normal"
                  helperText="Obligatoire"
                  style={{marginBottom:"20px",width:"100%"}}
                  InputProps={{
                    readOnly: true,
                  }}
                  InputLabelProps={{
                    shrink: true,
                  }}
                />
              </Grid>
             
            </Grid>
            </form>
          </Paper>
        </Grid>
        <Snackbar
        anchorOrigin={{
          vertical: 'bottom',
          horizontal: 'left',
        }}
        open={this.state.open}
        autoHideDuration={60000}
        onClose={this.handleClose}
        ContentProps={{
          'aria-describedby': 'message-id',
        }}
        message={<span id="message-id">{this.state.message}</span>}
        action={[
          ,
          <IconButton
            key="close"
            aria-label="Close"
            color="inherit"
            className={classes.close}
            onClick={this.handleClose}
          >
            <CloseIcon />
          </IconButton>,
        ]}
      />
            <Grid item xs={12}>
            {this.state.isLoad && (this.state.regularisation_employee != null && this.state.regularisation_employee.length > 0)
                                    && <Regularisationemployee data={this.state.regularisation_employee}></Regularisationemployee>}
         
          </Grid>
      </Grid>
    );
  }
}

RegularisationForm.propTypes = {
  classes: PropTypes.object.isRequired,
  isAdd:PropTypes.object.isRequired,
};

export default withStyles(styles)(RegularisationForm);
