import React, {Component} from 'react';
import classNames from 'classnames';
import PropTypes from 'prop-types';
import Avatar from '@material-ui/core/Avatar';
import Button from '@material-ui/core/Button';
import CssBaseline from '@material-ui/core/CssBaseline';
import FormControl from '@material-ui/core/FormControl';
import FormControlLabel from '@material-ui/core/FormControlLabel';
import Checkbox from '@material-ui/core/Checkbox';
import Input from '@material-ui/core/Input';
import InputLabel from '@material-ui/core/InputLabel';
import LockOutlinedIcon from '@material-ui/icons/LockOutlined';
import Paper from '@material-ui/core/Paper';
import Typography from '@material-ui/core/Typography';
import withStyles from '@material-ui/core/styles/withStyles';
import AuthService from '../AuthHelperMethods';
import { withRouter } from "react-router-dom";
import axios from 'axios';

const styles = theme => ({
  main: {
    width: 'auto',
    display: 'block', // Fix IE 11 issue.
    marginLeft: theme.spacing.unit * 3,
    marginRight: theme.spacing.unit * 3,
    [theme.breakpoints.up(400 + theme.spacing.unit * 3 * 2)]: {
      width: 400,
      marginLeft: 'auto',
      marginRight: 'auto',
    },
  },
  paper: {
    marginTop: theme.spacing.unit * 8,
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    padding: `${theme.spacing.unit * 2}px ${theme.spacing.unit * 3}px ${theme.spacing.unit * 3}px`,
  },
  avatar: {
    margin: theme.spacing.unit,
    backgroundColor: "#82BB25"
  },
  form: {
    width: '100%', // Fix IE 11 issue.
    marginTop: theme.spacing.unit,
  },
  submit: {
    marginTop: theme.spacing.unit * 3,
  },
});

class SignIn extends React.Component {
  constructor(){
      super();
      this.Auth = new AuthService();
      this.handleFormSubmit = this.handleFormSubmit.bind(this);
     this.handleChange = this.handleChange.bind(this);
  }
  componentWillMount(){
    if(this.Auth.loggedIn()) {
      if(this.Auth.getProfile().role == "User")
          this.props.history.replace('/tableau_de_borde');
      else
          this.props.history.replace('/Manage_User');
    }
        axios.get('/api/Users')
        .then(res => {
          console.log('ppppppppppppppppppppppppp');
        });
}

  handleFormSubmit(e){
    e.preventDefault();
    
    this.Auth.login(this.state.username,this.state.password)
        .then(res => {
          console.log(res);
           
           window.location.reload();
          })
            .catch (function (error) {
              if(error.message == "Not Found")
              alert("Le nom d'utilisateur ou le mot de passe n'est pas correct");
              else if(error.message == "Bad Request")
              alert("Le compte a été bloqué par l'administrateur veuillez le contactez");
              else
              alert(error.message);
            } 
      );
    
   
}

handleChange(e){
  this.setState(
      {
          [e.target.name]: e.target.value
      }
  )
}

render() {
  const { classes } = this.props;
  return (
    <main className={classes.main}>
      <CssBaseline />
      <Paper className={classes.paper}>
        <Avatar className={classes.avatar}>
          <LockOutlinedIcon />
        </Avatar>
        <Typography component="h1" variant="h5">
          Se Connecter
        </Typography>
        <form onSubmit={this.handleFormSubmit} className={classes.form}>
          <FormControl margin="normal" required fullWidth>
            <InputLabel htmlFor="username">Nom d'utilisateur:</InputLabel>
            <Input id="username" name="username" autoComplete="username" onChange={this.handleChange} autoFocus />
          </FormControl>
          <FormControl margin="normal" required fullWidth>
            <InputLabel htmlFor="password">Mot de passe</InputLabel>
            <Input name="password" type="password" id="password" onChange={this.handleChange} autoComplete="current-password" />
          </FormControl>
          <Button
            type="submit"
            fullWidth
            variant="contained"
            color="primary"
            className={classes.submit}
          >
            Se connecter
          </Button>
        </form>
      </Paper>
    </main>
  );
}
}

SignIn.propTypes = {
  classes: PropTypes.object.isRequired,
};

export default withRouter(withStyles(styles)(SignIn));