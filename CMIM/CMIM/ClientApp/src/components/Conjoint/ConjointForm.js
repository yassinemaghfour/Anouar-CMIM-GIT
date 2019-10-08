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
import Modal from '@material-ui/core/Modal';
import axios from 'axios';
import LinearProgress from '@material-ui/core/LinearProgress';
import DeleteIcon from '@material-ui/icons/Delete';
import EditIcon from '@material-ui/icons/Edit';
import { Link, Switch, NavLink} from 'react-router-dom';
import Snackbar from '@material-ui/core/Snackbar';
import CloseIcon from '@material-ui/icons/Close';
import Chip from '@material-ui/core/Chip';
import withAuth from '../withAuth.js';





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
    // boxShadow: theme.shadows[5],
    padding: theme.spacing.unit * 4,
  },
});
;

class ConjointForm extends React.Component {
  state = {
    multiline: 'Controlled',
    open:false,
    employee:'',
    add:true,
    update:false,
    isLoad:false,
    isClickable:true,
    open:false,
    message:'',
    selectedDate: new Date('2014-08-18T21:11:54'),
    employees:[],
    lname:'',
    fname:'',
    matriculecmim:'',
    id:'',
    open:false
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
    this.setState({update:true});
  }
  handleDeleteClick = () =>
  {
      if(this.state.id != '')
      {
          axios.delete('/api/Conjoints/'+this.state.id)
          .then(res => {
              this.setState({
                  message:'deleted',
                  open:true
              })
          })
      }
  }
  handleCancelClick = () => {

  }
  handleSubmitForm = () => {
      console.log('mmmmmmmmmmmmmmmmmmmmmmmmmm');
      console.log(this.state);
    // console.log(e.target.lname.value);
    // console.log(e.target.fname.value);
    // console.log(e.target.employee.value);
    // console.log(e.target.date.value);
    var data = {
        'firstname':this.state.fname,
        'lastname':this.state.lname,
        'employeematricule':this.state.employee,
        'matriculecmim':this.state.matriculecmim,
        'dateNs':this.state.date
    }
    if(!this.state.update && this.state.add)
    {
      console.log(data);
        axios.post('/api/Conjoints',data)
        .then(res => {
            console.log(res);
            this.setState({
                message:'Ajouter avec success',
                open:true,
                id:res.data.conjointId,
                add:false
            })
            this.refrechParent(res.data);
        });
    }
    else if(this.state.update && !this.state.add)
    {
        data.ConjointId = this.state.id;
        axios.put('/api/Conjoints/'+this.state.id,data)
        .then(res => {
            console.log(res);
            this.setState({
                message:'Modifier avec success',
                open:true,
                add:false,
                update:false,
            })
        });
    }
    
  }
  componentDidMount()
  {
    
    axios.get('/api/Employees')
    .then(res => {
        this.setState({
            employees:res.data
        });
    });
  }

  constructor(props)
  {
    super(props);
    this.refrechParent = this.refrechParent.bind(this);
  }
  refrechParent(data)
  {
    console.log('mmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm');
    console.log(this.props);

    // this.props.loadData(data);
  }
  render() {
    const { classes } = this.props;

    return (

      <Grid container style={{margin:"0 auto"}}>

        <Grid item xs={12}  >
          <Paper style={{boxShadow:" 0 0 0 0"}} >
          {this.state.isLoad && <LinearProgress color="primary" />}

          <form className={classes.container} Validate autoComplete="off" style={{textAlign:"center"}} >

          <Grid container xs={6} >
              <Typography variant="h5" gutterBottom style={{padding:'20px'}} >
                {this.state.id != '' ? this.state.referance : 'Nouvelle Conjoint'}
                  </Typography>
          </Grid>
          <Grid container xs={6} style={{justifyContent:'flex-end',padding:'18px'}}>
          {(this.state.add || this.state.update) &&
            <div>
            {!this.state.isLoad &&
              <Button variant="contained" onClick={this.handleSubmitForm} color="primary" className={classes.button}>
                    sauvegarde
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
            {this.state.referance == '' &&
              <Grid item xs={4} style={{padding:"0 30px"}}>

                <TextField
                  id="referance"
                  label="Referance"
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
                    id="employee"
                    select
                    label="Employee"
                    margin="normal"
                    className={classes.textField}
                    value={this.state.employee}
                    onChange={this.handleChange('employee')}
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
                    {this.state.employees.map(c => (
                     <MenuItem key={c.matricule} value={c.matricule}>
                                            {c.matricule}
                      </MenuItem>) )
                    }
                   </TextField>
              </Grid>
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
                  InputLabelProps={{
                    shrink: true,
                  }}
                />
              </Grid>
              <Grid  item xs={4} style={{padding:"0 100px"}} >
                <TextField
                  id="lname"
                  label="Nom"
                  value={this.state.montant}
                  className={classes.textField}
                  onChange={this.handleChange('lname')}
                  margin="normal"
                  helperText="Obligatoire"
                  style={{marginBottom:"20px",width:"100%"}}
                  InputProps={{
                    readOnly: !(this.state.add || this.state.update),
                  }}
                  InputLabelProps={{
                    shrink: true,
                  }}
                />
              </Grid>
             
                <Grid  item xs={4} style={{padding:"0 30px"}} >
                  <TextField
                    id="fname"
                    label="Prénom"
                    value={this.state.avance}
                    className={classes.textField}
                    onChange={this.handleChange('fname')}
                    margin="normal"
                    helperText="Obligatoire"
                    style={{marginBottom:"20px",width:"100%"}}
                    InputProps={{
                      readOnly: !(this.state.add || this.state.update),
                    }}
                    InputLabelProps={{
                      shrink: true,
                    }}
                  />
              </Grid>
              <Grid item xs={4} style={{padding:"0 30px"}}>
                <TextField
                  id="date"
                  label="Date de naissance"
                  value={this.formatDate(this.state.date)}
                  type="date"
                  className={classes.textField}
                  onChange={this.handleChange('date')}
                  margin="normal"
                  helperText="Obligatoire"
                  style={{marginBottom:"20px",width:"100%"}}
                  InputProps={{
                    readOnly: !(this.state.add || this.state.update),
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
      </Grid>
    );
  }
}

ConjointForm.propTypes = {
  classes: PropTypes.object.isRequired,
};

export default withAuth(withStyles(styles)(ConjointForm));
