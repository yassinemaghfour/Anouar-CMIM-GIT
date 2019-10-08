import React from 'react';
import classNames from 'classnames';
import MenuItem from '@material-ui/core/MenuItem';
import TextField from '@material-ui/core/TextField';
import PropTypes from 'prop-types';
import { withStyles } from '@material-ui/core/styles';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TablePagination from '@material-ui/core/TablePagination';
import TableRow from '@material-ui/core/TableRow';
import TableSortLabel from '@material-ui/core/TableSortLabel';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
import Paper from '@material-ui/core/Paper';
import Checkbox from '@material-ui/core/Checkbox';
import Grid from '@material-ui/core/Grid';
import IconButton from '@material-ui/core/IconButton';
import Tooltip from '@material-ui/core/Tooltip';
import Button from '@material-ui/core/Button';
import DeleteIcon from '@material-ui/icons/Delete';
import AddtIcon from '@material-ui/icons/Add';
import { lighten } from '@material-ui/core/styles/colorManipulator';
import EditIcon from '@material-ui/icons/Edit';
import SendIcon from '@material-ui/icons/Send';
import { Link, Switch, NavLink} from 'react-router-dom';
import LinearProgress from '@material-ui/core/LinearProgress';
import axios from 'axios';
import withAuth from '../withAuth.js';




function desc(a, b, orderBy) {
  if (b[orderBy] < a[orderBy]) {
    return -1;
  }
  if (b[orderBy] > a[orderBy]) {
    return 1;
  }
  return 0;
}

function stableSort(array, cmp) {
  const stabilizedThis = array.map((el, index) => [el, index]);
  stabilizedThis.sort((a, b) => {
    const order = cmp(a[0], b[0]);
    if (order !== 0) return order;
    return a[1] - b[1];
  });
  return stabilizedThis.map(el => el[0]);
}

function getSorting(order, orderBy) {
  return order === 'desc' ? (a, b) => desc(a, b, orderBy) : (a, b) => -desc(a, b, orderBy);
}

const rows = [
 
  { id: 'matricule', numeric: false, disablePadding: true, label: 'Matricule' },
  { id: 'matriculecmim', numeric: false, disablePadding: true, label: 'Matricule CMIM' },
  { id: 'last_name', numeric: true, disablePadding: false, label: 'Nom' },
  { id: 'first_name', numeric: true, disablePadding: false, label: 'Prénom' },
  { id: 'company', numeric: true, disablePadding: false, label: 'Société' },
  

  { id: 'view', numeric: true, disablePadding: false, label: '' },
];

class EnhancedTableHead extends React.Component {
  createSortHandler = property => event => {
    this.props.onRequestSort(event, property);
  };

  render() {
    const { onSelectAllClick, order, orderBy, numSelected, rowCount } = this.props;

    return (
      <TableHead>
        <TableRow>
          <TableCell padding="checkbox">
            <Checkbox
              indeterminate={numSelected > 0 && numSelected < rowCount}
              checked={numSelected === rowCount}
              onChange={onSelectAllClick}
            />
          </TableCell>
          {rows.map(row => {
            return (
              <TableCell
                key={row.id}
                align={row.numeric ? 'right' : 'left'}
                padding={row.disablePadding ? 'none' : 'default'}
                sortDirection={orderBy === row.id ? order : false}
              >
                <Tooltip
                  title="Sort"
                  placement={row.numeric ? 'bottom-end' : 'bottom-start'}
                  enterDelay={300}
                >
                  <TableSortLabel
                    active={orderBy === row.id}
                    direction={order}
                    onClick={this.createSortHandler(row.id)}
                  >
                    {row.label}
                  </TableSortLabel>
                </Tooltip>
              </TableCell>
            );
          }, this)}
        </TableRow>
      </TableHead>
    );
  }
}

EnhancedTableHead.propTypes = {
  numSelected: PropTypes.number.isRequired,
  onRequestSort: PropTypes.func.isRequired,
  onSelectAllClick: PropTypes.func.isRequired,
  order: PropTypes.string.isRequired,
  orderBy: PropTypes.string.isRequired,
  rowCount: PropTypes.number.isRequired,
};

const toolbarStyles = theme => ({
  root: {
    paddingRight: theme.spacing.unit,
  },
  highlight:
    theme.palette.type === 'light'
      ? {
          color: theme.palette.secondary.main,
          backgroundColor: lighten(theme.palette.secondary.light, 0.85),
        }
      : {
          color: theme.palette.text.primary,
          backgroundColor: theme.palette.secondary.dark,
        },
  spacer: {
    flex: '1 1 100%',
  },
  actions: {
    color: theme.palette.text.secondary,
  },
  title: {
    flex: '0 0 auto',
  },
  textField: {
    marginLeft: theme.spacing.unit,
    marginRight: theme.spacing.unit,
  }
});

let EnhancedTableToolbar = props => {
  const { numSelected, classes } = props;

  return (
    <Toolbar
      className={classNames(classes.root, {
        [classes.highlight]: numSelected > 0,
      })}
    >
      <div className={classes.title}>
        {numSelected > 0 ? (
          <Typography color="inherit" variant="subtitle1">
            {numSelected} selected
          </Typography>
        ) : (
          <Typography variant="h6" id="tableTitle">
            Liste des employés
          </Typography>
        )}
      </div>
      <div className={classes.spacer} />
      <div className={classes.actions}>
        {numSelected > 0 ? (
          <Tooltip title="Delete">
            <IconButton aria-label="Delete">
              <DeleteIcon />
            </IconButton>
          </Tooltip>
        ) : (
          <Link to="/employees/nouveau" >
            <Tooltip title="Ajouter nouveau employee">
              <IconButton aria-label="Ajouter">
                <AddtIcon />
              </IconButton>
            </Tooltip>
          </Link>

        )}
      </div>
    </Toolbar>
  );
};

EnhancedTableToolbar.propTypes = {
  classes: PropTypes.object.isRequired,
  numSelected: PropTypes.number.isRequired,
};

EnhancedTableToolbar = withStyles(toolbarStyles)(EnhancedTableToolbar);

const styles = theme => ({
  root: {
    width: '100%',
    marginTop: theme.spacing.unit * 3,
  },
  table: {
    // minWidth: 1020,
  },
  tableWrapper: {
    overflowX: 'auto',
  },
  textField: {
    marginLeft: theme.spacing.unit,
    marginRight: theme.spacing.unit,
  }
});

class EnhancedTable extends React.Component {
  state = {
    order: 'asc',
    orderBy: 'calories',
    selected: [],
    data: [
      
    ],
    activityPalace: [],
    activityPlace: -1,
    BU: [],
    bu: -1,
    societe: 'all',
    page: 0,
    rowsPerPage: 5,
  };
  componentDidMount()
  {
    fetch('api/Employees')
      .then(response => response.json())
      .then(data => {
        console.log(data);
        this.setState({ data: data });
      });
    axios.get(`/api/ActivityPlaces`)
    .then(res => {
      if(res.status == 200)
      {
        console.log("Activity places: ");
        console.log(res);
        this.setState({
          activityPalace: res.data
        });
        console.log("datas: ");
        console.log(this.state);
      }
  });  
    axios.get(`/api/bu`)   
    .then(res => {
    if(res.status == 200)
    {
    
      this.setState({
        BU: res.data
      });
   
    }
});    
  }
  handleChange = name => event => {
    this.setState({
      [name]: event.target.value,
    });
    console.log("statut");
    console.log(this.state);
  };
  handleChangeAct = name => event => {
    this.setState({
      activityPlace: event.target.value,
      bu: event.target.value,
    });
    console.log("statut");
    console.log(this.state);
  };
  handleRequestSort = (event, property) => {
    const orderBy = property;
    let order = 'desc';

    if (this.state.orderBy === property && this.state.order === 'desc') {
      order = 'asc';
    }

    this.setState({ order, orderBy });
  };

  handleSelectAllClick = event => {
    if (event.target.checked) {
      this.setState(state => ({ selected: state.data.map(n => n.matricule) }));
      return;
    }
    this.setState({ selected: [] });
  };

  handleClick = (event, id) => {
    const { selected } = this.state;
    const selectedIndex = selected.indexOf(id);
    let newSelected = [];

    if (selectedIndex === -1) {
      newSelected = newSelected.concat(selected, id);
    } else if (selectedIndex === 0) {
      newSelected = newSelected.concat(selected.slice(1));
    } else if (selectedIndex === selected.length - 1) {
      newSelected = newSelected.concat(selected.slice(0, -1));
    } else if (selectedIndex > 0) {
      newSelected = newSelected.concat(
        selected.slice(0, selectedIndex),
        selected.slice(selectedIndex + 1),
      );
    }

    this.setState({ selected: newSelected });
  };

  handlefiltreClick = (event) => {
    axios.get("api/Employees/" + this.state.societe + "/" + this.state.activityPlace)
    .then(res => {
      if(res.status == 200)
      {
        console.log("filtre result: ");
        this.setState({ data: res.data });
        console.log("datas: ");
        console.log(this.state);
      }
  });
    
  };
  
 /* handlefiltreClick = (event) => {
    axios.get("api/bu/" + this.state.societe + "/" + this.state.bu)
    .then(res => {
      if(res.status == 200)
      {
        console.log("filtre result: ");
        this.setState({ data: res.data });
        console.log("datas: ");
        console.log(this.state);
      }
  });
    
  };*/

  handleChangePage = (event, page) => {
    this.setState({ page });
  };

  handleChangeRowsPerPage = event => {
    this.setState({ rowsPerPage: event.target.value });
  };

  isSelected = id => this.state.selected.indexOf(id) !== -1;

  render() {
    const { classes } = this.props;
    const { data, order, orderBy, selected, rowsPerPage, page } = this.state;
    const emptyRows = rowsPerPage - Math.min(rowsPerPage, data.length - page * rowsPerPage);

    return (
      
      <Paper className={classes.root} style={{padding: "10px 0px"}}>
      <Grid container xs={6} >
              <Typography variant="h5" gutterBottom style={{padding:'20px'}} >
              Filter les employés par:
              </Typography>
      </Grid>
      <Grid container xs={12} style={{padding: "0px 20px", textAlign: 'center'}} >
      <Grid container xs={5} >
      <TextField
                    id="societe"
                    select
                    label="La société"
                    className={classes.textField}
                    value={this.state.societe}
                    onChange={this.handleChange('societe')}
                    SelectProps={{
                      MenuProps: {
                        className: classes.menu,
                      },
                    }}
                    margin='normal'
                    style={{width:"100%", margin: "0px 5px "}}
                    >
                    <MenuItem key="all" value="all">Tous les sociétés</MenuItem>
                    <MenuItem key="VEM" value="VEM">VEM</MenuItem>
                    <MenuItem key="VEAS" value="VEAS">VEAS</MenuItem>
                    <MenuItem key="SVL" value="SVL">SVL</MenuItem>
                    }
                   </TextField>
        </Grid>
        <Grid container xs={5} >
        <TextField
                    id="activityPalace"
                    select
                    label="Lieu d'activité"
                    onChange={this.handleChangeAct('activityPalace')}
                    className={classes.textField}
                    value={this.state.activityPlace}
                    SelectProps={{
                      MenuProps: {
                        className: classes.menu,
                      },
                    }}
                    margin='normal'
                    style={{width:"100%", margin: "0px 5px "}}
                    >
                                        <MenuItem value='-1' key='-1'>Tous les lieux d'activités</MenuItem>

                    {this.state.activityPalace.map(c => (
                     <MenuItem key={c.placdeId} value={c.placdeId}>
                                            {c.name}
                      </MenuItem>) )
                    }
                    
                    }
                   </TextField></Grid>


                   <Grid container xs={5} >
        <TextField
                    id="BU"
                    select
                    label="B.U"
                    onChange={this.handleChangeAct('BU')}
                    className={classes.textField}
                    value={this.state.bu}
                    SelectProps={{
                      MenuProps: {
                        className: classes.menu,
                      },
                    }}
                    margin='normal'
                    style={{width:"100%", margin: "0px 5px "}}
                    >
                                        <MenuItem value='-1' key='-1'>les B.U</MenuItem>

                    {this.state.BU.map(c => (
                     <MenuItem key={c.buId} value={c.buId}>
                                            {c.name}
                      </MenuItem>) )
                    }
                    
                    }
                   </TextField></Grid>
                  
                  
           <Grid container xs={2} style={{justifyContent:'flex-end',padding:'18px'}}>
           <Button variant="contained" width="100%" style={{width:"100%"}}  onClick={this.handlefiltreClick} type="submit" color="primary" className={classes.button}>
                    Filtrer
              </Button>
           </Grid>
                   
                   </Grid>
<hr/>
        <EnhancedTableToolbar numSelected={selected.length} />
        <div className={classes.tableWrapper}>
          <Table className={classes.table} aria-labelledby="tableTitle">
            <EnhancedTableHead
              numSelected={selected.length}
              order={order}
              orderBy={orderBy}
              onSelectAllClick={this.handleSelectAllClick}
              onRequestSort={this.handleRequestSort}
              rowCount={data.length}
            />
            <TableBody>
              {stableSort(data, getSorting(order, orderBy))
                .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                .map(n => {
                  const isSelected = this.isSelected(n.matricule);
                  return (
                    <TableRow
                      hover
                      onClick={event => this.handleClick(event, n.matricule)}
                      role="checkbox"
                      aria-checked={isSelected}
                      tabIndex={-1}
                      key={n.matricule}
                      selected={isSelected}
                    >
                      <TableCell padding="checkbox">
                        <Checkbox checked={isSelected} />
                      </TableCell>

                      <TableCell align="left">{n.matricule}</TableCell>
                      
                      <TableCell align="right">{n.matriculecmim}</TableCell>
                      <TableCell align="right">{n.last_name}</TableCell>
                      <TableCell align="right">{n.first_name}</TableCell>
                      <TableCell align="right">{n.company}</TableCell>
                      <TableCell align="right">{n.bu}</TableCell>
                      <TableCell align="left">
                        <Link to={{pathname:'/employees/'+n.matricule,state:{matricule:n.matricule,matcmim:n.matriculecmim,lname:n.last_name,fname:n.first_name,adresse:n.adresse,bu:n.bu,update:false,add:false}}}>
                          <Tooltip title="Afficher">
                            <IconButton className={classes.button} component="span" onClick={this.handleUpdateClick}>
                              <SendIcon/>
                            </IconButton>
                          </Tooltip>
                        </Link>
                      </TableCell>
                    </TableRow>
                  );
                })}
              {emptyRows > 0 && (
                <TableRow style={{ height: 49 * emptyRows }}>
                  <TableCell colSpan={6} />
                </TableRow>
              )}
            </TableBody>
          </Table>
        </div>
        <TablePagination
          rowsPerPageOptions={[5, 10, 25]}
          component="div"
          count={data.length}
          rowsPerPage={rowsPerPage}
          page={page}
          backIconButtonProps={{
            'aria-label': 'Previous Page',
          }}
          nextIconButtonProps={{
            'aria-label': 'Next Page',
          }}
          onChangePage={this.handleChangePage}
          onChangeRowsPerPage={this.handleChangeRowsPerPage}
        />
      </Paper>
    );
  }
}

EnhancedTable.propTypes = {
  classes: PropTypes.object.isRequired,
};

export default withAuth(withStyles(styles)(EnhancedTable));
