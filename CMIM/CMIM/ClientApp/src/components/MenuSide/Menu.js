import React from 'react';
import PropTypes from 'prop-types';
import classNames from 'classnames';
import { withStyles } from '@material-ui/core/styles';
import Drawer from '@material-ui/core/Drawer';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import List from '@material-ui/core/List';

import CssBaseline from '@material-ui/core/CssBaseline';
import Typography from '@material-ui/core/Typography';
import Divider from '@material-ui/core/Divider';
import IconButton from '@material-ui/core/IconButton';
import MenuIcon from '@material-ui/icons/Menu';
import ChevronLeftIcon from '@material-ui/icons/ChevronLeft';
import ChevronRightIcon from '@material-ui/icons/ChevronRight';
import ListItem from '@material-ui/core/ListItem';
import ListItemIcon from '@material-ui/core/ListItemIcon';
import ListItemText from '@material-ui/core/ListItemText';
import InboxIcon from '@material-ui/icons/MoveToInbox';
import MailIcon from '@material-ui/icons/Mail';
import SearchIcon from '@material-ui/icons/Search';
import InputBase from '@material-ui/core/InputBase';
import { fade } from '@material-ui/core/styles/colorManipulator';
import MenuItem from '@material-ui/core/MenuItem';
import Menu from '@material-ui/core/Menu';
import AccountCircle from '@material-ui/icons/AccountCircle';
import Book from '@material-ui/icons/Book'
import Backup from '@material-ui/icons/Backup'
import Receipt from '@material-ui/icons/Receipt'
import Input from '@material-ui/icons/Input'
import Payment from '@material-ui/icons/Payment'
import SvgIcon from '@material-ui/core/SvgIcon';
import { createMuiTheme } from '@material-ui/core/styles';
import purple from '@material-ui/core/colors/purple';
import { Link, Switch, NavLink,Route} from 'react-router-dom';

import EnhancedTable from '../Employees/Employees';
import EmployeesForm from '../Employees/EmployeesForm';
import Dossies from '../Dossies/Dossies.js';
import DossierForm from '../Dossies/DossierForm';
import FullWidthGrid from '../Dashboard/Dashboard';
import bordereaux from '../Borderaux/bordereaux';
import bordereauxForm from '../Borderaux/bordereauxForm';
import regularisation from '../Regularisation/regularisation';
import RegularisationForm from '../Regularisation/regularisationForm'
import MyDropzone from '../Test';
import NavTabs from '../Envoyerdossier/Envoi';
import EnhancedTable3 from '../remborssement/remborssement';
import EnhancedTableHead2 from '../remborssement/RembourssementForm';
import Acti from '../Act/Activite';
import Actiform from '../Act/ActiForm';
import Site from '../Site/Site';
import SiteForm from '../Site/SiteForm';
import bu from '../Bu/Bu';
import Buform from '../Bu/BuForm';
import Home from '@material-ui/icons/Home';
import WorkIcon from '@material-ui/icons/Work';
import Emplo from '../TelechrEmployer/TelecharEmployer';
import signIn from '../Login/signIn';
import AuthService from "../AuthHelperMethods";
import { withRouter } from "react-router-dom";
import Users from '../Users/Users.js';
import UserForm from '../Users/UserForm.js';

import page404 from "../404Page.js";

const drawerWidth = 300;

const styles = theme => ({


  root: {
    display: 'flex',
  },

  appBar: {
    zIndex: theme.zIndex.drawer + 1,
    transition: theme.transitions.create(['width', 'margin'], {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.leavingScreen,
    }),
    backgroundColor: '#82BB25',
  },
  appBarShift: {
    marginLeft: drawerWidth,
    width: `calc(100% - ${drawerWidth}px)`,
    transition: theme.transitions.create(['width', 'margin'], {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.enteringScreen,
    }),
    backgroundColor: '#82BB25',
  },
  menuButton: {
    marginLeft: 12,
    marginRight: 36,
  },
  hide: {
    display: 'none',
  },
  drawer: {
    width: drawerWidth,
    flexShrink: 0,
    whiteSpace: 'nowrap',
  },
  drawerOpen: {
    width: drawerWidth,
    transition: theme.transitions.create('width', {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.enteringScreen,
    }),
  },
  searchIcon: {
    width: theme.spacing.unit * 9,
    height: '100%',
    position: 'absolute',
    pointerEvents: 'none',
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'center',
  },
  inputRoot: {
    color: 'inherit',
    width: '100%',
  },
  search: {
    position: 'relative',
    borderRadius: theme.shape.borderRadius,
    backgroundColor: fade(theme.palette.common.white, 0.15),
    '&:hover': {
      backgroundColor: fade(theme.palette.common.white, 0.25),
    },
    marginRight: theme.spacing.unit * 2,
    marginLeft: 0,
    width: '100%',
    [theme.breakpoints.up('sm')]: {
      marginLeft: theme.spacing.unit * 3,
      width: 'auto',
    },
  },
  inputInput: {
    paddingTop: theme.spacing.unit,
    paddingRight: theme.spacing.unit,
    paddingBottom: theme.spacing.unit,
    paddingLeft: theme.spacing.unit * 10,
    transition: theme.transitions.create('width'),
    width: '100%',
    [theme.breakpoints.up('md')]: {
      width: 200,
    },
  },
  sectionDesktop: {
    display: 'none',
    [theme.breakpoints.up('md')]: {
      display: 'flex',
    },
  },
  sectionMobile: {
    display: 'flex',
    [theme.breakpoints.up('md')]: {
      display: 'none',
    },
  },
  drawerClose: {
    transition: theme.transitions.create('width', {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.leavingScreen,
    }),
    overflowX: 'hidden',
    width: theme.spacing.unit * 7 + 1,
    [theme.breakpoints.up('sm')]: {
      width: theme.spacing.unit * 9 + 1,
    },
  },
  toolbar: {
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'flex-end',
    padding: '0 8px',
   
    ...theme.mixins.toolbar,
  },
  content: {
    flexGrow: 1,
    padding: theme.spacing.unit * 3,
  },
  menuItem: {
    '&:focus': {
      backgroundColor: theme.palette.primary.main,
      '& $primary, & $icon': {
        color: theme.palette.common.white,
      },
    },
  },
});

class MiniDrawer extends React.Component {
  constructor(){
    super();
    this.Auth = new AuthService();
}
    state = {
    open: false,
    auth: false,
    anchorEl: null,
    selectedIndex: 0,
    isLoad:true,
    isAdmin: false
  };



  handleListItemClick = (event, index) => {
    this.setState({ selectedIndex: index });

  };

  handleDrawerOpen = () => {
    this.setState({ open: true });
  };

  handleDrawerClose = () => {
    this.setState({ open: false });
  };

  handleMenu = event => {
    this.setState({ anchorEl: event.currentTarget });
  };

  handleClose = () => {
    this.setState({ anchorEl: null });
    console.log(this.state.auth);
  };

  seDeconnecter = () => {
    this.Auth.logout();
    this.handleClose();
    this.setState({
      auth: false
    });
    this.props.history.replace("/SignIn");
  }; 

  componentDidMount() {
    this.setState({
      isLoad:true
    })
    if(this.Auth.loggedIn()) {
        this.setState({
          auth: true,
          isAdmin: this.Auth.getProfile().role == "Admin" ? true : false
        });
    }
    else {
      this.setState({
        auth: false
      });
    }
  }
  render() {
    const { classes, theme } = this.props;
    const { anchorEl,isLoad } = this.state;
    const open = Boolean(anchorEl);
    return (
      <div className={classes.root}>
        <CssBaseline />
        <AppBar
          position="fixed"
          className={classNames(classes.appBar, {
            [classes.appBarShift]: this.state.open,
          })}
        >
          <Toolbar disableGutters={!this.state.open}>
            <IconButton
              color="inherit"
              aria-label="Open drawer"
              onClick={this.handleDrawerOpen}
              className={classNames(classes.menuButton, {
                [classes.hide]: this.state.open,
              })}
            >
              <MenuIcon />
            </IconButton>
            <Typography variant="h6" color="inherit" noWrap>
              Vivo Energy - CMIM
            </Typography>
            <div className={classes.search}>
              <div className={classes.searchIcon}>
                <SearchIcon />
              </div>
              <InputBase
                placeholder="Recherche…"
                classes={{
                  root: classes.inputRoot,
                  input: classes.inputInput,
                }}
              />
            </div>
            {this.state.auth && (
              <div style={{position: 'absolute',right: 0}}>
                <IconButton
                  aria-owns={open ? 'menu-appbar' : undefined}
                  aria-haspopup="true"
                  onClick={this.handleMenu}
                  color="inherit"
                >
                  <AccountCircle />
                </IconButton>
                <Menu
                  id="menu-appbar"
                  anchorEl={anchorEl}
                  anchorOrigin={{
                    vertical: 'top',
                    horizontal: 'right',
                  }}
                  transformOrigin={{
                    vertical: 'top',
                    horizontal: 'right',
                  }}
                  open={open}
                  onClose={this.handleClose}
                >
                  <MenuItem onClick={this.handleClose}>Profile</MenuItem>
                  <MenuItem onClick={this.handleClose}>My account</MenuItem>
                  <MenuItem onClick={event => this.seDeconnecter()}>Se déconnecter</MenuItem>
                </Menu>
              </div>
            )}

          </Toolbar>

        </AppBar>
        <Drawer
          variant="permanent"
          className={classNames(classes.drawer, {
            [classes.drawerOpen]: this.state.open,
            [classes.drawerClose]: !this.state.open,
          })}
          classes={{
            paper: classNames({
              [classes.drawerOpen]: this.state.open,
              [classes.drawerClose]: !this.state.open,
            }),
          }}
          open={this.state.open}
        >
          <div className={classes.toolbar}>
            <IconButton onClick={this.handleDrawerClose}>
              {theme.direction === 'rtl' ? <ChevronRightIcon /> : <ChevronLeftIcon />}
            </IconButton>
          </div>
          <Divider />
          {this.state.auth && (  <List>
         
            {!this.state.isAdmin && (<div><NavLink to="/tableau_de_borde" style={{textDecoration:"none"}}><ListItem button key="chart"
            selected={this.state.selectedIndex === 0}
            onClick={event => this.handleListItemClick(event, 0) }>
                <ListItemIcon><SvgIcon><path d="M19 3H5c-1.1 0-2 .9-2 2v14c0 1.1.9 2 2 2h14c1.1 0 2-.9 2-2V5c0-1.1-.9-2-2-2zM9 17H7v-7h2v7zm4 0h-2V7h2v10zm4 0h-2v-4h2v4z"/><path d="M0 0h24v24H0z" fill="none"/></SvgIcon></ListItemIcon>
                <ListItemText primary="Tableau de bord" />
            </ListItem>

            </NavLink>
            <Link to="/dossiers" style={{textDecoration:"none"}}> <ListItem button key="dossiers" selected={this.state.selectedIndex === 1}
            onClick={event => this.handleListItemClick(event, 1)}>
                <ListItemIcon><SvgIcon><path d="M21.99 8c0-.72-.37-1.35-.94-1.7L12 1 2.95 6.3C2.38 6.65 2 7.28 2 8v10c0 1.1.9 2 2 2h16c1.1 0 2-.9 2-2l-.01-10zM12 13L3.74 7.84 12 3l8.26 4.84L12 13z"/><path d="M0 0h24v24H0z" fill="none"/></SvgIcon></ListItemIcon>
                <ListItemText primary="Les Dossiers" />
            </ListItem></Link>

            <Link to="/bordereaux" style={{textDecoration:"none"}}> <ListItem button key="bordereaux" selected={this.state.selectedIndex === 3}
            onClick={event => this.handleListItemClick(event, 3)}>
                <ListItemIcon><Book/></ListItemIcon>
                <ListItemText primary="Les Bordereaux" />
            </ListItem></Link>
            <Link to="/NavTabs" style={{ textDecoration: "none" }}>
              <ListItem button key="rembourssement" selected={this.state.selectedIndex === 7}
                 onClick={event => this.handleListItemClick(event, 7)}>
                   <ListItemIcon> <Receipt /> </ListItemIcon>
                   <ListItemText primary="Envoi à la CMIM" />
              </ListItem>
             </Link>     
            <Link to="/rembourssement" style={{textDecoration:"none"}}> <ListItem button key="dossiers" selected={this.state.selectedIndex === 4}
            onClick={event => this.handleListItemClick(event, 4)}>
                <ListItemIcon><Payment/></ListItemIcon>
                <ListItemText primary="Les remboursements" />
            </ListItem>

                    </Link>

                    <Link to="/regularisation" style={{ textDecoration: "none" }}> <ListItem button key="regularisations" selected={this.state.selectedIndex === 5}
                        onClick={event => this.handleListItemClick(event, 5)}>
                        <ListItemIcon><Input /></ListItemIcon>
                        <ListItemText primary="Les régularisations" />
                    </ListItem>

                    </Link>

<Link to="/ImporterRembourssement" style={{ textDecoration: "none" }}>
                        <ListItem button key="rembourssement" selected={this.state.selectedIndex === 6}
                            onClick={event => this.handleListItemClick(event, 6)}>
                            <ListItemIcon> <Backup/> </ListItemIcon>
                            <ListItemText primary="Importer un Remboursement" />
                        </ListItem>
                    </Link>

<Link to="/Emplo" style={{ textDecoration: "none" }}>
                        <ListItem button key="rembourssement" selected={this.state.selectedIndex === 8}
                            onClick={event => this.handleListItemClick(event, 8)}>
                            <ListItemIcon><SvgIcon> <Backup/></SvgIcon> </ListItemIcon>
                            <ListItemText primary="Importer liste des employés" />
                        </ListItem>
                    </Link>

                   

            <Link to="/employees" style={{textDecoration:"none"}}>
            <ListItem button key="employees" selected={this.state.selectedIndex === 2}
            onClick={event => this.handleListItemClick(event, 2)}>
                <ListItemIcon> <SvgIcon><path d="M16 11c1.66 0 2.99-1.34 2.99-3S17.66 5 16 5c-1.66 0-3 1.34-3 3s1.34 3 3 3zm-8 0c1.66 0 2.99-1.34 2.99-3S9.66 5 8 5C6.34 5 5 6.34 5 8s1.34 3 3 3zm0 2c-2.33 0-7 1.17-7 3.5V19h14v-2.5c0-2.33-4.67-3.5-7-3.5zm8 0c-.29 0-.62.02-.97.05 1.16.84 1.97 1.97 1.97 3.45V19h6v-2.5c0-2.33-4.67-3.5-7-3.5z"/></SvgIcon> </ListItemIcon>
                <ListItemText primary="Les employés" />
            </ListItem>
            </Link>

            <Link to="/Activity" style={{ textDecoration: "none" }}>
                        <ListItem button key="rembourssement" selected={this.state.selectedIndex === 9}
                            onClick={event => this.handleListItemClick(event, 9)}>
                            <ListItemIcon> <SvgIcon><Home/> </SvgIcon></ListItemIcon>
                            <ListItemText primary="Les lieux d'activités" />
                        </ListItem>
            </Link>
            <Link to="/bu" style={{ textDecoration: "none" }}>
                <ListItem button key="rembourssement" selected={this.state.selectedIndex === 25}
                    onClick={event => this.handleListItemClick(event, 25)}>
                        <ListItemIcon> <SvgIcon><WorkIcon/> </SvgIcon></ListItemIcon>
                        <ListItemText primary="Les B.U" />
                </ListItem>
            </Link>
           
           </div>)}

            
            {this.state.isAdmin && (<div>
              <Link to="/Manage_User" style={{textDecoration:"none"}}>
            <ListItem button key="users" selected={this.state.selectedIndex === 26}
            onClick={event => this.handleListItemClick(event, 26)}>
                <ListItemIcon> <SvgIcon><path d="M16 11c1.66 0 2.99-1.34 2.99-3S17.66 5 16 5c-1.66 0-3 1.34-3 3s1.34 3 3 3zm-8 0c1.66 0 2.99-1.34 2.99-3S9.66 5 8 5C6.34 5 5 6.34 5 8s1.34 3 3 3zm0 2c-2.33 0-7 1.17-7 3.5V19h14v-2.5c0-2.33-4.67-3.5-7-3.5zm8 0c-.29 0-.62.02-.97.05 1.16.84 1.97 1.97 1.97 3.45V19h6v-2.5c0-2.33-4.67-3.5-7-3.5z"/></SvgIcon> </ListItemIcon>
                <ListItemText primary="Gestion des utilisateurs" />
            </ListItem>
            </Link>

            <Link to="/Site" style={{ textDecoration: "none" }}>
                <ListItem button key="rembourssement" selected={this.state.selectedIndex === 24}
                    onClick={event => this.handleListItemClick(event, 24)}>
                        <ListItemIcon> <SvgIcon><InboxIcon/> </SvgIcon></ListItemIcon>
                        <ListItemText primary="Les Sites" />
                </ListItem>
            </Link>

            </div>)}

          </List>)}

        </Drawer>

        <main className={classes.content}>
          <div className={classes.toolbar} />
            <div>
              {!this.state.isAdmin && 
               (<Switch>
                   
                   
                    <Route exact path="/" component={FullWidthGrid} />
                    <Route exact path="/SignIn" component={signIn} />
                    <Route exact path="/tableau_de_borde" component={FullWidthGrid}  />
                    <Route exact path="/employees" component={EnhancedTable} />
                
                    <Route exact path="/employees/nouveau" component={() => <EmployeesForm isAdd="true" />} />
                    <Route path="/employees/:mat" component={EmployeesForm}/>
                    <Route exact path="/dossiers" component={Dossies}/>
                    <Route exact path="/bordereaux" component={bordereaux}/>
                    <Route exact path="/bordereaux/:brdID" component={bordereauxForm}/>
                    <Route exact path="/regularisation" component={regularisation}/>
                    <Route exact path="/regularisation/nouveau" component={() => <RegularisationForm isAdd="True"  />}/>
                    <Route exact path="/regularisation/:regulID" component={RegularisationForm}/>
                    <Route path="/rembourssement" component={EnhancedTable3} />
                    <Route path="/Remboussement/:mat" component={EnhancedTableHead2} />
                    <Route exact path="/ImporterRembourssement" component={MyDropzone} />
                    <Route exact path="/NavTabs" component={NavTabs} />
                    <Route exact path="/dossiers/nouveau" component={() => <DossierForm isAdd="true" />}/>
                    <Route path="/dossiers/:ref" component={DossierForm}/>
                    <Route path="/Activity" component={Acti} />
                    <Route exact path="/Actiform/nouveau" component={() => <Actiform isAdd="true" />} />
                    <Route path="/Actiform/:mat" component={Actiform} />
                   
                    <Route path="/Bu" component={bu} />
                    <Route exact path="/Buform/nouveau" component={() => <Buform isAdd="true" />} />
                    <Route path="/Buform/:mat" component={Buform} />
                    <Route path="/Emplo" component={Emplo} />
                    <Route path="*" component={page404} />
                
                    
              </Switch>)}
              {this.state.isAdmin && 
               (<Switch>
                
                  <Route exact path="/" component={Users} />
                  <Route exact path="/SignIn" component={signIn} />

                  <Route exact path="/Manage_User" component={Users} />
                  <Route exact path="/Manage_User/nouveau" component={() => <UserForm isAdd="true" />} />
                  <Route exact path="/Manage_User/:id" component={UserForm} />

                  <Route path="/Site" component={Site} />
                    <Route exact path="/SiteForm/nouveau" component={() => <SiteForm isAdd="true" />} />
                    <Route path="/SiteForm/:mat" component={SiteForm} />

                    <Route path="*" component={page404} />

               </Switch>)}
            </div>
        </main>
      </div>
    );
  }
}

MiniDrawer.propTypes = {
  classes: PropTypes.object.isRequired,
  theme: PropTypes.object.isRequired,
};

export default withRouter(withStyles(styles, { withTheme: true })(MiniDrawer));
