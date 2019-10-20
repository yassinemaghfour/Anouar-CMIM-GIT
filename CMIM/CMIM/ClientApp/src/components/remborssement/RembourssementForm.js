import React from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import TextField from '@material-ui/core/TextField';
import Paper from '@material-ui/core/Paper';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import axios from 'axios';
import LinearProgress from '@material-ui/core/LinearProgress';
import Dossies from '../UserControl/UC_Dossiers';

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


class EnhancedTableHead2 extends React.Component {
    state = {
        multiline: 'Controlled',
        company: 'vem',
        matricule:'',
        date:'',
        bourdoreau:'',
        isLoad: false,
        isClickable: true,
        dossies: {},
        value: 0,
        mount: false,
    };

    refrechConjoints(conjoint) {
        var con = this.state.conjoints;
        con.push(conjoint);
        this.setState({
            conjoints: con
        })

    }
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
    formatDate = (date) => {
        var d = new Date(date),
            month = '' + (d.getMonth() + 1),
            day = '' + d.getDate(),
            year = d.getFullYear();

        if (month.length < 2) month = '0' + month;
        if (day.length < 2) day = '0' + day;

        return [day, month, year].join('-');
    };

    handleClose = () => {
        this.setState({ open: false });
    };

    componentDidMount() {
        console.log("Rembourssement ID: " + this.props.match.params.mat);
        axios.get(`/api/Remboussement/${this.props.match.params.mat}`)
            .then(res => {
                console.log('yesyes');
                console.log(res.data);
                var dos = [];                                                                                       
                // for (var i = 0; i < res.data.list.length; i++) {
                //     dos.push(res.data.list[i].dossier);
                // }
                this.setState({

                        'matricule': res.data.rembourssemnt.rembourssementId,
                       
                       'date': res.data.rembourssemnt.dateRembourssement,
                        'isLoad': false,
                        'dossies': res.data.dossiers,     
                })
                console.log('yesyes2');
                console.log(this.state.dossies);

            });
    }



    constructor(props) {
        super(props);
        if (props.data != undefined) {
            this.state = {
                multiline: 'Controlled',
                matricule: '',
                date: '',
                bourdoreau: '',     
                isLoad: false,
                isClickable: true,
                dossies: {},
                value: 0,
                mount: false,
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
                                <Grid container xs={7} >
                                    <Typography variant="h5" gutterBottom style={{ padding: '20px' }} >
                                        Remboursement N° : {this.state.matricule}
                                    </Typography>
                                </Grid>
                                <Grid item xs={6} style={{ padding: "0 30px" }} >
                                    <TextField
                                        id="matricule"
                                        label="N° Remboursement :"
                                        value={this.state.matricule}
                                        className={classes.textField}
                                        margin="normal"                                     
                                        style={{ marginBottom: "20px", width: "100%" }}                                     
                                    />
                                </Grid>
                                <Grid item xs={6} style={{ padding: "0 30px" }} >
                                    <TextField
                                        id="date"
                                        label="Date Remboursement :"
                                        value={this.formatDate(this.state.date)}
                                        className={classes.textField}     
                                        margin="normal"
                                        style={{ marginBottom: "20px", width: "100%" }}                                  
                                    />
                                </Grid>
                                <Grid item xs={12}>

                                    {!this.state.isLoad && (this.state.dossies != null && this.state.dossies.length > 0)
                                    && <Dossies data={this.state.dossies} />}
                                                         
                                </Grid>
                            </form>
                        </Paper>
                    </Grid>
                </Grid>
            </div>
        );
    }
}

EnhancedTableHead2.propTypes = {
    classes: PropTypes.object.isRequired,
};

export default withStyles(styles)(EnhancedTableHead2);
