import React from 'react';
import PropTypes from 'prop-types';
import classNames from 'classnames';
import { withStyles } from '@material-ui/core/styles';
import MenuItem from '@material-ui/core/MenuItem';
import TextField from '@material-ui/core/TextField';
import Paper from '@material-ui/core/Paper';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import IconButton from '@material-ui/core/IconButton';
import Tooltip from '@material-ui/core/Tooltip';
import Button from '@material-ui/core/Button';
import axios from 'axios';
import LinearProgress from '@material-ui/core/LinearProgress';
import DeleteIcon from '@material-ui/icons/Delete';
import EditIcon from '@material-ui/icons/Edit';
import Snackbar from '@material-ui/core/Snackbar';
import CloseIcon from '@material-ui/icons/Close';
import ConjointList from '../Conjoint/ConjointList.js';
import Dossies from '../Dossies/Dossies.js';
import Tabs from '@material-ui/core/Tabs';
import Tab from '@material-ui/core/Tab';
import AuthHelperMethods from "../AuthHelperMethods";
import { withRouter } from "react-router-dom";
import Enfant from '../Enfant/Enfants';


function TabContainer(props) {
  return (
    <Typography component="div" style={{ padding: 8 * 3 }}>
      {props.children}
    </Typography>
  );
}

TabContainer.propTypes = {
  children: PropTypes.node.isRequired,
};
function rand() {
  return Math.round(Math.random() * 20) - 10;
}
function getModalStyle() {
  const top = 50;
  const left = 50 ;
  console.log('sssssssssssssssssssssssssssssssssssssssssssssssssssssssssss')
  return {
    top: `${top}%`,
    left: `${left}%`,
    transform: `translate(-${top}%, -${left}%)`,
  };
}
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
  table: {
    minWidth: 700,
  },
  paper: {
    position: 'absolute',
    width: theme.spacing.unit * 50,
    backgroundColor: theme.palette.background.paper,
    boxShadow: theme.shadows[5],
    padding: theme.spacing.unit * 4,
    padding: theme.spacing.unit * 2,
    textAlign: 'center',
    color: theme.palette.text.secondary,
  },
  tabsRoot: {
    borderBottom: '1px solid #e8e8e8',
  },
  tabsIndicator: {
    backgroundColor: '#1890ff',
  },
  tabRoot: {
    textTransform: 'initial',
    minWidth: 72,
    fontWeight: theme.typography.fontWeightRegular,
    marginRight: theme.spacing.unit * 4,
    fontFamily: [
      '-apple-system',
      'BlinkMacSystemFont',
      '"Segoe UI"',
      'Roboto',
      '"Helvetica Neue"',
      'Arial',
      'sans-serif',
      '"Apple Color Emoji"',
      '"Segoe UI Emoji"',
      '"Segoe UI Symbol"',
    ].join(','),
    '&:hover': {
      color: '#40a9ff',
      opacity: 1,
    },
    '&$tabSelected': {
      color: '#1890ff',
      fontWeight: theme.typography.fontWeightMedium,
    },
    '&:focus': {
      color: '#40a9ff',
    },
  },
  tabSelected: {},
});

const companys = [
    {
        value: 'vem',
        label:'VEM'
    },
    {
        value: 'veas',
        label: 'VEAS'
    },
    {
        value: 'svl',
        label: 'SVL'
    }
]

class EmployeesForm extends React.Component {
  state = {
    multiline: 'Controlled',
    company: 'vem',
    open:false,
    matricule:'',
    matriculecmim:'',
    lname:'',
    fname:'',
    adresse:'',
    place:'',
    places:[],
    bu:'',
    bus:[],
    add:false,
    update:false,
    isLoad:false,
    isClickable:true,
    open:true,
    message:'',
    conjoints:[],
    dossies:[],
    value:0,
    mount:false,
    open:false
  };

  refrechConjoints (conjoint){
    var con = this.state.conjoints;
    con.push(conjoint);
    this.setState({
      conjoints:con
    })

  }
  handleTabChange = (event, value) => {
    this.setState({ value });
  };
  handleChange = name => event => {
    console.log(name);
    console.log(event);
    
    
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
    axios.delete('/api/employees/'+this.state.matricule)
      .then(res => {
        console.log(res);
        if(res.status == 200)
        {
          this.props.history.push("/employees");
        }
      })
  }
  handleCancelClick = () => {
    this.setState({
      update:false
    })
  }
handleSubmit = (e) => {
  this.setState({
    isLoad:true
  });

  console.log(e.target.company.value);
  const Auth = new AuthHelperMethods();
  const data = {
    'matricule': this.state.matricule == ''?  e.target.mat.value : this.state.matricule ,
    'matriculecmim': e.target.matriculecmim.value,
    'first_name':e.target.fname.value,
    'last_name':e.target.lname.value,
    'adresse':e.target.adresse.value,
    'company':e.target.company.value,
    'placeplacdeid':e.target.place.value,
    'buId':e.target.bu.value,
    'UserId': Auth.getProfile().unique_name
  }
  console.log(data);
  if(!this.state.update && this.state.add)
  {
    axios.post('/api/employees',data)
      .then(res => {
        if (res.status == 201)
        {
          this.setState({
            open:true,
            message:"Ajoute avec succes",
            matricule:res.data.matricule
          });
        }
        console.log(res);
      })
  }
  else if (this.state.update && !this.state.add) {
    axios.put("/api/employees/"+this.state.matricule,data)
      .then(res => {
        if(res.status == 204)
        {
          this.setState({
            open:true,
            message:"Modifier avec succes"
          });
        }
        console.log(res);
      })
  }

    this.setState({
      isLoad:false,
      add:false,
      update:false
    });
    e.preventDefault();
    return false;

  }
  componentDidMount()
  {
    const Auth = new AuthHelperMethods();
    if (!Auth.loggedIn()) {
        this.props.history.replace('/SignIn');
        return;
    }
    console.log("User: ");
    console.log(Auth.getProfile());
    console.log(this.props.isAdd);
    console.log(this.state);
    axios.get('/api/bu')
    .then(res => {
      console.log('ppppppppppppppppppppppppp');
      console.log(res);
      this.setState({
        bus:res.data
      });
    });
    axios.get('/api/ActivityPlaces')
    .then(res => {
      console.log('ppppppppppppppppppppppppp');
      console.log(res);
      this.setState({
        places:res.data
      });
    });
    if (this.props.isAdd == "true")
      return;
    this.setState({
      matricule:this.props.match.params.mat
    });
    console.log(this.props.match.params.mat);
    axios.get(`/api/employees/${this.props.match.params.mat}`)
    .then(res => {
      console.log('nnnnnnn');
      console.log(res);
      console.log(res);
      this.setState({
        'lname':res.data.last_name,
        'fname':res.data.first_name,
        'matriculecmim':res.data.matriculecmim,
        'company':res.data.company.toLowerCase(),
        'adresse':res.data.adresse,
        place:res.data.placeplacdeId,
        bu:res.data.buId,
        enfants:res.data.enfants
      })
      console.log("employee data");
      console.log(res.data.placdeId);
      axios.get("/api/Dossiers/emp/"+this.props.match.params.mat)
      .then(res => {
          
          this.setState({
            dossies:res.data,
            
          });
      });
      console.log('hiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiii');
      axios.get("/api/Conjoints/emp/"+this.props.match.params.mat)
      
          .then(res => {
            this.setState({
              conjoints:res.data,
              'mount':true 
            });
          });
      axios.get("/api/Enfants/emp/"+this.props.match.params.mat)
      
          .then(res => {
            this.setState({
              enfants:res.data,
              'mount':true 
            });
          });
      
    });

    

    console.log(this.state);
  }
  constructor(props)
  {
     super(props);
     this.refrechConjoints = this.refrechConjoints.bind(this);
     if(props.isAdd == "true")
     {
       this.state = { multiline: 'Controlled',
       company: 'vem',
       open:false,
       matricule:'',
       matriculecmim:'',
       lname:'',
       fname:'',
       adresse:'',
       place:'',
       places:[],
       bu:'',
       bus:[],
       add:true,
       update:false,
       isLoad:false,
       isClickable:true,
       open:true,
       message:'',
       conjoints:{},
       dossies:{},
       value:0,
       mount:false,
       open:false};
     }
     if(props.data != undefined)
     {
       this.state = {multiline: 'Controlled',
       company: 'vem',
       open:false,
       matricule:'',
       matriculecmim:'',
       lname:'',
       fname:'',
       adresse:'',
       place:'',
       places:[],
       bu:'',
       bus:[],
       add:false,
       update:false,
       isLoad:false,
       isClickable:true,
       open:true,
       message:'',
       conjoints:{},
       dossies:{},
       value:0,
       mount:false,
       open:false,
      data:props.data}
     }
    //  this.state = {places:[]}

  }
  render() {
    const { classes } = this.props;
    const { value,mount } = this.state;
    console.log(this.state);
    return (

      <div>
      <Grid container style={{width:"90%",margin:"0 auto"}}>

        <Grid item xs={12}  >
          <Paper>
          {this.state.isLoad && <LinearProgress color="primary" />}

          <form className={classes.container} Validate autoComplete="off" style={{textAlign:"center"}} onSubmit={this.handleSubmit} >
          <Grid container xs={6} >
            <Typography variant="h5" gutterBottom style={{padding:'20px'}} >
              {this.state.matricule != '' ? this.state.matricule : 'Nouvelle employé'}
            </Typography>
          </Grid>
          <Grid container xs={6} style={{justifyContent:'flex-end',padding:'18px'}}>
          {(this.state.add || this.state.update) &&
            <div>
              <Button variant="contained"  type="submit" color="primary" className={classes.button}>
                    sauvegarde
              </Button>
              	&nbsp;	&nbsp;
              <Button className={classes.button} onClick={this.handleCancelClick}>
                    annuler
              </Button>
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
          {(!this.state.add && !this.state.update) &&
            (
                <Tooltip title="Modifier" aria-label="Modifier">
                  <IconButton className={classes.button} component="span" onClick={this.handleUpdateClick}>
                    <EditIcon/>
                  </IconButton>
                </Tooltip>
            )
          }
          </Grid>
            <Grid container  xs={12}  >
            {this.state.matricule == '' &&
              <Grid item xs={4} style={{padding:"0 30px"}}>

                <TextField
                  id="mat"
                  label="Matricule"
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
          
          <Grid  item xs={4} style={{padding:"0 30px"}} >
                <TextField
                  id="matriculecmim"
                  label="Matricule CMIM"
                  value={this.state.matriculecmim}
                  className={classes.textField}
                  onChange={this.handleChange('matriculecmim')}
                  margin="normal"
                  helperText="Obligatoire"
                  style={{marginBottom:"20px",width:"100%"}}
                  InputProps={{
                    readOnly: !(this.state.add || this.state.update),
                  }}
                />
              </Grid>

              <Grid item xs={4} style={{padding:"0 30px"}}>
                  <TextField
                    id="company"
                    select
                    label="Société"
                    margin="normal"
                    className={classes.textField}
                    value={this.state.company}
                    onChange={this.handleChange('company')}
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
                    >
                    {companys.map(c => (
                     <MenuItem key={c.value} value={c.value}>
                                            {c.label}
                      </MenuItem>) )
                    }
                   </TextField>
              </Grid>
              <Grid item xs={4} style={{padding:"0 30px"}}>
                <TextField
                id="lname"
                label="Nom"
                value={this.state.lname}
                className={classes.textField}
                onChange={this.handleChange('lname')}
                margin="normal"
                helperText="Obligatoire"
                style={{marginBottom:"20px",width:"100%"}}
                InputProps={{
                  readOnly: !(this.state.add || this.state.update),
                }}
                />
              </Grid>
              <Grid  item xs={4} style={{padding:"0 30px"}} >
                <TextField
                  id="fname"
                  label="Prénom"
                  value={this.state.fname}
                  className={classes.textField}
                  onChange={this.handleChange('fname')}
                  margin="normal"
                  helperText="Obligatoire"
                  style={{marginBottom:"20px",width:"100%"}}
                  InputProps={{
                    readOnly: !(this.state.add || this.state.update),
                  }}
                />
              </Grid>

            

              
              <Grid item xs={4} style={{padding:"0 30px"}}>
                  <TextField
                    id="place"
                    select
                    label="Lieu d'activité"
                    margin="normal"
                    className={classes.textField}
                    value={this.state.place}
                    onChange={this.handleChange('place')}
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
                    >
                    {this.state.places.map(c => (
                     <MenuItem key={c.placdeId} value={c.placdeId}>
                                            {c.name}
                      </MenuItem>) )
                    }
                   </TextField>
              </Grid>
              <Grid item xs={4} style={{padding:"0 30px"}}>
                  <TextField
                    id="bu"
                    select
                    label="B.U"
                    margin="normal"
                    className={classes.textField}
                    value={this.state.bu}
                    onChange={this.handleChange('bu')}
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
                    >
                    {this.state.bus.map(c => (
                     <MenuItem key={c.buId} value={c.buId}>
                                            {c.name}
                      </MenuItem>) )
                    }
                   </TextField>
              </Grid>
              <Grid  item xs={this.state.matricule == '' ? 4 : 8} style={{padding:"0 30px"}} >
                <TextField
                id="adresse"
                label="Adresse"
                multiline
                onChange={this.handleChange('adresse')}
                value={this.state.adresse}
                className={classes.textField}
                margin="normal"
                helperText="Obligatoire"
                style={{width:"100%"}}
                InputProps={{
                  readOnly:!(this.state.add || this.state.update),
                }}
                />
              </Grid >
              <Grid item xs={12}>
              {mount && 
                <Tabs
                value={value}
                onChange={this.handleTabChange}
                classes={{ root: classes.tabsRoot, indicator: classes.tabsIndicator }}>
                      <Tab
                        disableRipple
                        classes={{ root: classes.tabRoot, selected: classes.tabSelected }}
                        label="Dossies"/>
                        <Tab
                        disableRipple
                        classes={{ root: classes.tabRoot, selected: classes.tabSelected }}
                        label="Conjoints"/>
                        <Tab
                        disableRipple
                        classes={{ root: classes.tabRoot, selected: classes.tabSelected }}
                        label="Enfants"/>
                </Tabs>
                }
                  {mount && 
                    <div>
                      {console.log(this.state)}
                      {value === 0 && <TabContainer><Dossies data={this.state.dossies} /></TabContainer>}
                      {value === 1 && <TabContainer><ConjointList data={this.state.conjoints}  /></TabContainer>}
                      {value === 2 && <TabContainer><Enfant data={this.state.enfants} /></TabContainer>}
                    </div>
                  }
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
      </Grid>
      </div>
    );
  }
}

EmployeesForm.propTypes = {
  classes: PropTypes.object.isRequired,
};

export default withRouter(withStyles(styles)(EmployeesForm));
