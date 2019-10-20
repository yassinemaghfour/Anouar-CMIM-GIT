import React from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import TextField from '@material-ui/core/TextField';
import Paper from '@material-ui/core/Paper';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import IconButton from '@material-ui/core/IconButton';
import axios from 'axios';
import LinearProgress from '@material-ui/core/LinearProgress';
import Snackbar from '@material-ui/core/Snackbar';
import CloseIcon from '@material-ui/icons/Close';
import Dossies from '../UserControl/UC_Dossiers';
import AuthHelperMethods from "../AuthHelperMethods";
import { withRouter } from "react-router-dom";

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

class bordereauxForm extends React.Component {
  state = {
    multiline: 'Controlled',
    open:false,
    bordereauId: "",
    dateCreation:'',
    company: '',
    montant:'',
    isLoad:false,
    isClickable:true,
    open:true,
    message:'',
    dossiers: []
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

  handleCancelClick = () => {

  }
handleSubmit = (e) => {

  
    this.setState({
      isLoad:true
    });
  

  const data = {
    'referance': this.state.referance == '' ?  e.target.referance.value : this.state.referance ,
    'employeematricule':e.target.employee.value,
    'date':e.target.date.value,
    'montant':e.target.montant.value,
  };
  console.log("this data");
  console.log(data);
  console.log("end of this data data");
  if(!this.state.update && this.state.add)
  {
    axios.post('/api/Dossiers',data)
      .then(res => {
        if (res.status == 201)
        {
          
            this.setState({
              open:true,
              message:"Ajoute avec succes",
              referance:res.data.referance,
              avance:res.data.avance,
              diff:res.data.diff
            });
          
        }
        console.log(res);
      });
  }
  else if (this.state.update && !this.state.add) {
    axios.put("/api/Dossiers/"+this.state.referance,data)
      .then(res => {
        if(res.status == 204)
        {

          this.setState({
            open:true,
            message:"Modifier avec succes",
            avance:(this.state.montant * 0.8),
            diff:(this.state.montant - (this.state.montant * 0.8))
          });
        }
        console.log(res);
      });
  }

    this.setState({
      isLoad:false,
      add:false,
      update:false
    });

  e.preventDefault();
  }

getBordereauInfo = async () => {
  let res = await axios.get(`/api/Bordereaux/${this.props.match.params.brdID}`);
    if(res.status == 200)
    {
      console.log("Res");
      console.log(res);
      this.setState({
        dateCreation: new Date(res.data.dateCreation) ,
        company: res.data.company ,
        dossiers: res.data.dossiers,
        isLoad : false
      });
    }
}

  componentDidMount()
  {
    const Auth = new AuthHelperMethods();
    if (!Auth.loggedIn()) {
        this.props.history.replace('/SignIn');
        return;
    }
      this.setState({
        bordereauId:this.props.match.params.brdID
      });

      axios.get(`/api/Bordereaux/${this.props.match.params.brdID}`)
      .then(res => {
        if(res.status == 200)
      {
        console.log("Res");
        console.log(res);
        this.setState({
          dateCreation: new Date(res.data.dateCreation) ,
          company: res.data.company ,
          dossiers: res.data.dossiers,
          isLoad : false
        });
      }
      });
      
  }

  constructor(props)
  {
     super(props);
     this.state = {
       employees: [],
       dossiers:[],
       bordereauId: props.match.params.brdID,
       etat:'ouvert',
       add:props.isAdd == "true" ? true : false,
       
     }
     console.log(props);
  }
  render() {
    const { classes } = this.props;

    return (

      <Grid container style={{width:"90%",margin:"0 auto"}}>

        <Grid item xs={12}  >
          <Paper>
          {this.state.isLoad && <LinearProgress color="primary" />}

          <form className={classes.container} Validate autoComplete="off" style={{textAlign:"center"}} onSubmit={this.handleSubmit} >

          <Grid container xs={6} >
              <Typography variant="h5" gutterBottom style={{padding:'20px'}} >
                Référence du bordereau: {this.state.bordereauId }
              </Typography>
          </Grid>
          
            <Grid container  xs={12}  >

              <Grid item xs={6} style={{padding:"0 30px"}}>
                  <TextField
                    id="dateCreation"
                    label="Date Création Du Bordereau"
                    margin="normal"
                    className={classes.textField}
                    value={this.formatDate(this.state.dateCreation)}
                    margin="normal"
                    style={{width:"200px",width:"100%"}}
                    InputProps={{
                      readOnly: true
                    }}
                    InputLabelProps={{
                      shrink: true,
                    }}
                    >
                   </TextField>
              </Grid>
              <Grid item xs={6} style={{padding:"0 30px"}}>
                <TextField
                  id="company"
                  label="La Société Concernée"
                  value={this.state.company}
                  inputStyle={{ textAlign: 'center', cursor: 'none' }}
                  className={classes.textField}
                  margin="normal"
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
       {!this.state.isLoad && (this.state.dossiers != null && this.state.dossiers.length > 0)
                                    && <Dossies data={this.state.dossiers} />}
          
          </Grid>
      </Grid>
         
    );
  }
}

bordereauxForm.propTypes = {
  classes: PropTypes.object.isRequired,
};

export default withRouter(withStyles(styles)(bordereauxForm));
