import React from 'react';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import AppBar from '@material-ui/core/AppBar';
import Tabs from '@material-ui/core/Tabs';
import NoSsr from '@material-ui/core/NoSsr';
import Tab from '@material-ui/core/Tab';
import Typography from '@material-ui/core/Typography';
import Envoi from '../Envoyerdossier/EnvoyerForm';
import Avance from '../Envoyerdossier/AvanceForm';
import withAuth from '../withAuth.js';


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

function LinkTab(props) {
    return <Tab component="a" onClick={event => event.preventDefault()} {...props} />;
}

const styles = theme => ({
    root: {
        flexGrow: 1,
        backgroundColor: theme.palette.background.paper,
    },
    tabsRoot: {
        borderBottom: '1px solid #000000',
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
          color: '#000000',
          opacity: 1,
        },
        '&$tabSelected': {
          color: '#000000',
          fontWeight: theme.typography.fontWeightMedium,
        },
        '&:focus': {
          color: '#000000',
        },
      },
});

class NavTabs extends React.Component {
    state = {
        value: 0,
    };

    handleChange = (event, value) => {
        this.setState({ value });
    };

    render() {
        const { classes } = this.props;
        const { value } = this.state;

        return (
            <NoSsr >
                <div className={classes.root}>
                    <AppBar position="static">
                        <Tabs variant="fullWidth" value={value} onChange={this.handleChange}
                         classes={{ root: classes.tabsRoot, indicator: classes.tabsIndicator }}
                        >
                            <LinkTab style={{color: "#ffffff"}} label="Les dossiers envoyés à la CMIM" href="page1" />
                            <LinkTab style={{color: "#ffffff"}} label=" Les dossiers non envoyés à la CMIM" href="page2" />
                        </Tabs>
                    </AppBar>
                    {value === 0 && <TabContainer><Envoi /></TabContainer>}
                    {value === 1 && <TabContainer><Avance/></TabContainer>}
                </div>
            </NoSsr>
        );
    }
}

NavTabs.propTypes = {
    classes: PropTypes.object.isRequired,
};

export default withAuth(withStyles(styles)(NavTabs));