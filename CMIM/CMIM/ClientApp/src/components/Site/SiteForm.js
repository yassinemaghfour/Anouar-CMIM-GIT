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

import withAuth from '../withAuth.js';
import { withRouter } from "react-router-dom";

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
    const left = 50;
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



class SiteForm extends React.Component {
    state = {
        multiline: 'Controlled',
        open: false,
        SiteId: '',
        name: '',  
        add: false,
        update: false,
        isLoad: false,
        isClickable: true,
        open: true,
        message: '',
        value: 0,
        mount: false,
        open: false
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
            'name': e.target.name.value,
        }

        if (!this.state.update && this.state.add) {
            axios.post('/api/Site', data)
                .then(res => {
                    if (res.status == 201) {
                        this.setState({
                            open: true,
                            message: "Ajouter avec succès",
                            SiteId: res.data.siteId
                        });
                    }
                    console.log(res);
                })
        }
        else if (this.state.update && !this.state.add) {
            
            console.log(this.state.SiteId);

            data.SiteId = this.state.SiteId;
                axios.put(`/api/Site/${this.state.SiteId}`, data)
                .then(res => {
                    
                    if (res.status == 204) {
                        this.setState({
                            open: true,
                            message: "Modifier avec succes"
                        });
                     
                    }
                    console.log(res);
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

    componentDidMount() {
        if (this.props.isAdd == "true")
            return;
        this.setState({
            SiteId: this.props.match.params.mat
        });
        console.log(this.props.match.params.mat);
        axios.get(`/api/Site/${this.props.match.params.mat}`)
            .then(res => {
                console.log('nnnnnnn');
                console.log(res);

                this.setState({
                    'SiteId': res.data.siteId,
                    'name': res.data.name,
                    'mount': true,
                })
            });



        console.log(this.state);
    }

    constructor(props) {
        super(props);
        if (props.isAdd == "true") {
            this.state = {
                multiline: 'Controlled',
                open: false,
                SiteId: '',
                name: '',           
                add: true,
                update: false,
                isLoad: false,
                isClickable: true,
                open: true,
                message: '',
                value: 0,
                mount: false,
                open: false
            };
        }
        if (props.data != undefined) {
            this.state = {
                multiline: 'Controlled',
                company: 'vem',
                open: false,
                SiteId: '',
                name: '',
                add: false,
                update: false,
                isLoad: false,
                isClickable: true,
                open: true,
                message: '',
                value: 0,
                mount: false,
                open: false,
                data: props.data
            }
        }
        //  this.state = {places:[]}

    }
    render() {
        const { classes } = this.props;
        const { value, mount } = this.state;
        console.log(this.state);
        return (

            <div>
                <Grid container style={{ width: "90%", margin: "0 auto" }}>

                    <Grid item xs={12}  >
                        <Paper>
                            {this.state.isLoad && <LinearProgress color="primary" />}

                            <form className={classes.container} Validate autoComplete="off" style={{ textAlign: "center" }} onSubmit={this.handleSubmit} >

                                <Grid container xs={6} >
                                    <Typography variant="h5" gutterBottom style={{ padding: '20px' }} >
                                        {this.state.SiteId != '' ? 'Code de site : '+this.state.SiteId : 'Nouveau site'}
                                    </Typography>
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
                                   
                              
                                    <Grid item xs={6} style={{ padding: "0 30px" }}>
                                        <TextField
                                            id="name"
                                            label="Nom du Site"
                                            value={this.state.name}
                                            className={classes.textField}
                                            onChange={this.handleChange('name')}
                                            margin="normal"
                                            helperText="Obligatoire"
                                            style={{ marginBottom: "20px", width: "100%" }}
                                            InputProps={{
                                                readOnly: !(this.state.add || this.state.update),
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
            </div>
        );
    }
}

SiteForm.propTypes = {
    classes: PropTypes.object.isRequired,
};

export default withStyles(styles)(SiteForm);
