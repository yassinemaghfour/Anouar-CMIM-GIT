import React from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
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
import MenuItem from '@material-ui/core/MenuItem';
import Switch from "react-switch";


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

class UserForm extends React.Component {

    state = {
        multiline: 'Controlled',
        open: false,
        UserId: '',
        Lname: '',  
        Fname: '',  
        Username: '', 
        Email: '',
        Etat: true,
        add: false,
        Sites: [],
        site: '',
        update: false,
        isLoad: false,
        isClickable: true,
        open: true,
        message: '',
        value: 0,
        mount: false,
        open: false,
        siteLoaded : false
    };



    
    handleTabChange = (event, value) => {
        this.setState({ value });
    };
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
    handleUpdateClick = () => {
        this.setState({
            update: true
        })
    }

    handleDeleteClick = () => {
        axios.delete('/api/Site/' + this.state.SiteId)
            .then(res => {
                console.log(res);
                if (res.status == 200) {
                    this.props.history.push("/Site");
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
            isLoad: true
        });

        const data = {
            'Id': this.state.UserId,
             'FirstName': e.target.Fname.value,
             'LastName': e.target.Lname.value,
             'Username' : e.target.Username.value, 
             'Email': e.target.Email.value,
             'Password': e.target.Pass.value,
             'Etat': this.state.Etat,
             'siteId': e.target.site.value, 
             'Role': 'User'
        }

        if (!this.state.update && this.state.add) {
            console.log("Post Data");
            console.log(data);
            axios.post('/api/Users', data)
                .then(res => {
                    if (res.status == 201) {
                        this.setState({
                            open: true,
                            message: "Ajouter avec succès",
                            UserId: res.data.id
                        });
                    }
                    console.log("Post result");
                    console.log(res);
                })
        }
        else if (this.state.update && !this.state.add) {
            
            console.log(this.state.UserId);

                axios.put(`/api/Users/${this.state.UserId}`, data)
                .then(res => {
                    
                    if (res.status == 204) {
                        this.setState({
                            open: true,
                            message: "Modifier avec succes"
                        });
                        console.log(res);
                    }
                   
                })
        }

        this.setState({
            isLoad: false,
            add: false,
            update: false
        });
        e.preventDefault();
        return false;

    }

    swicthhandleChange(Etat) {
        this.setState({Etat});
    }

    componentDidMount() {
        axios.get(`/api/Site`)
    .then(res => {
      this.setState({
        Sites : res.data,
        siteLoaded : true
      });
      console.log("site loaded");
      console.log(this.state);
    });
        if (this.props.isAdd == "true")
            return;
        this.setState({
            UserId: this.props.match.params.id
        });
        console.log("ID: ");
        console.log(this.props.match.params.id);
        
        axios.get(`/api/Users/${this.props.match.params.id}`)
            .then(res => {
                console.log('nnnnnnn');
                console.log(res);

                this.setState({
                    'UserId': res.data.id,
                    'Lname': res.data.lastName,  
                    'Fname': res.data.firstName,  
                    'Username': res.data.username, 
                    'Email': res.data.email,
                    'site': res.data.site == null ? "" : res.data.site.siteId,
                    'Etat': res.data.etat,
                    'Pass': res.data.password,
                    'mount': true,
                })
                
console.log("DataUser");
console.log(this.state);
            });
    }

    constructor(props) {
        super(props);
        this.swicthhandleChange = this.swicthhandleChange.bind(this);
        if (props.isAdd == "true") {
            this.state = {
                multiline: 'Controlled',
                open: false,
                UserId: '',
                name: '',           
                add: true,
                Etat: true,
                update: false,
                isLoad: false,
                isClickable: true,
                open: true,
                message: '',
                Pass: '',
                Email: '',
                open: false
            };
        }
        if (props.data != undefined) {
            this.state = {
                multiline: 'Controlled',
                company: 'vem',
                open: false,
                UserId: '',
                name: '',
                Etat: true,
                add: false,
                update: false,
                isLoad: false,
                isClickable: true,
                open: true,
                message: '',
                Pass: '',
                Email: '',
                open: false,
                data: props.data
            }
        }
        //  this.state = {places:[]}

    }
    render() {
        const { classes } = this.props;
        console.log(this.state);
        return (

            <div>
                <Grid container style={{ width: "90%", margin: "0 auto" }}>

                    <Grid item xs={12}  >
                        <Paper>
                            {this.state.isLoad && <LinearProgress color="primary" />}

                            <form className={classes.container} Validate autoComplete="off" style={{ textAlign: "center" }} onSubmit={this.handleSubmit} >

                                <Grid container xs={6} style={{ padding: '20px' }} >
                                    <Typography variant="h5" gutterBottom style={{ paddingRight: '20px' }}  >
                                        {this.state.UserId != '' ? 'Code d\'utilisateur : '+ this.state.UserId : 'Nouveau utilisateur'}
                                    </Typography>
                                    <Switch
                                disabled = {!(this.state.add || this.state.update)}
          onChange={this.swicthhandleChange}
          checked={this.state.Etat}
          offColor="#AF0404"
          onColor="#207561"
          id="normal-switch"
        />
                                </Grid>
                              
                                <Grid container xs={6} style={{ justifyContent: 'flex-end', padding: '18px' }}>
                                    {(this.state.add || this.state.update) &&
                                        <div>
                                            <Button variant="contained" type="submit" color="primary" className={classes.button}>
                                                Sauvegarde
              </Button>
                                            &nbsp;	&nbsp;
              <Button className={classes.button} onClick={this.handleCancelClick}>
                                                Annuler
              </Button>
                                        </div>

                                    }
                                    {(!this.state.add && !this.state.update) &&
                                        (
                                            <Tooltip title="Supprimer" aria-label="Supprimer">
                                                <IconButton className={classes.button} component="span" onClick={this.handleDeleteClick} >
                                                    <DeleteIcon />
                                                </IconButton>
                                            </Tooltip>

                                        )
                                    }
                                    {(!this.state.add && !this.state.update) &&
                                        (
                                            <Tooltip title="Modifier" aria-label="Modifier">
                                                <IconButton className={classes.button} component="span" onClick={this.handleUpdateClick}>
                                                    <EditIcon />
                                                </IconButton>
                                            </Tooltip>
                                        )
                                    }
                                </Grid>
                                <Grid container xs={12}  >
                                   
                              
                                    <Grid item xs={4} style={{ padding: "0 30px" }}>
                                        <TextField
                                            id="Lname"
                                            label="Nom de l'utilisateur"
                                            value={this.state.Lname}
                                            className={classes.textField}
                                            onChange={this.handleChange('Lname')}
                                            margin="normal"
                                            helperText="Obligatoire"
                                            style={{ marginBottom: "20px", width: "100%" }}
                                            InputProps={{
                                                readOnly: !(this.state.add || this.state.update),
                                            }}
                                        />
                                    </Grid>

                                    <Grid item xs={4} style={{ padding: "0 30px" }}>
                                        <TextField
                                            id="Fname"
                                            label="Prenom de l'utilisateur"
                                            value={this.state.Fname}
                                            className={classes.textField}
                                            onChange={this.handleChange('Fname')}
                                            margin="normal"
                                            helperText="Obligatoire"
                                            style={{ marginBottom: "20px", width: "100%" }}
                                            InputProps={{
                                                readOnly: !(this.state.add || this.state.update),
                                            }}
                                        />
                                    </Grid>

                                    <Grid item xs={4} style={{ padding: "0 30px" }}>
                                        <TextField
                                            id="Username"
                                            label="Username de connexion"
                                            value={this.state.Username}
                                            className={classes.textField}
                                            onChange={this.handleChange('Username')}
                                            margin="normal"
                                            helperText="Obligatoire"
                                            style={{ marginBottom: "20px", width: "100%" }}
                                            InputProps={{
                                                readOnly: !(this.state.add || this.state.update),
                                            }}
                                        />
                                    </Grid>

                                    <Grid item xs={4} style={{ padding: "0 30px" }}>
                                        <TextField
                                            id="Email"
                                            label="Email"
                                            value={this.state.Email}
                                            className={classes.textField}
                                            onChange={this.handleChange('Email')}
                                            margin="normal"
                                            helperText="Obligatoire"
                                            style={{ marginBottom: "20px", width: "100%" }}
                                            InputProps={{
                                                readOnly: !(this.state.add || this.state.update),
                                            }}
                                            InputLabelProps={{
                                                shrink: true,
                                              }}
                                            
                                        />
                                    </Grid>

                                    <Grid item xs={4} style={{ padding: "0 30px" }}>
                                        <TextField
                                            id="Pass"
                                            label="Mot de passe"
                                            type="password"
                                            className={classes.textField}
                                            onChange={this.handleChange('Pass')}
                                            margin="normal"
                                            
                                            style={{ marginBottom: "20px", width: "100%"
                                            , visibility: this.state.add ? 'true' : 'Hidden' }}
                                            InputProps={{
                                                readOnly: !(this.state.add || this.state.update),
                                                autoComplete:"new-password",
                                                form: {
                                                    autocomplete: 'off'
                                                }
                                            }}

                                        />
                                    </Grid>

                                    <Grid item xs={4} style={{padding:"0 30px"}}>
                      {this.state.siteLoaded && <TextField
                        id="site"
                        select
                        label="Site"
                        margin="normal"
                        onChange={this.handleChange('site')}
                        className={classes.textField}
                        value={this.state.site}
                        SelectProps={{
                          MenuProps: {
                            className: classes.menu,
                          },
                        }}
                        
                        margin="normal"
                        style={{width:"200px",width:"100%"}}
                        InputProps={{
                            readOnly: !(this.state.add || this.state.update)
                          }}
                        InputLabelProps={{
                          shrink: true,
                        }}
                        >
                        {this.state.Sites.map(c => (
                        <MenuItem key={c.siteId} value={c.siteId}>
                                                {c.name}
                          </MenuItem>) )
                        }
                      </TextField>}
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

UserForm.propTypes = {
    classes: PropTypes.object.isRequired,
};

export default withStyles(styles)(UserForm);
