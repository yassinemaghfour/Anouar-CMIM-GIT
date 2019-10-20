import React, {Component} from 'react';
import classNames from 'classnames';
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
import IconButton from '@material-ui/core/IconButton';
import Tooltip from '@material-ui/core/Tooltip';
import DeleteIcon from '@material-ui/icons/Delete';
import AddtIcon from '@material-ui/icons/Add';
import { lighten } from '@material-ui/core/styles/colorManipulator';
import SendIcon from '@material-ui/icons/Send';
import { Link, Switch, NavLink} from 'react-router-dom';
import axios from 'axios';
import LinearProgress from '@material-ui/core/LinearProgress';


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
  { id: 'referance', numeric: false, disablePadding: true, label: 'Référence' },
  { id: 'employee', numeric: true, disablePadding: false, label: 'Employé' },
  { id: 'date', numeric: true, disablePadding: false, label: 'Date' },
 
  { id: 'montant', numeric: true, disablePadding: false, label: 'Montant Total' },
  { id: 'avance', numeric: true, disablePadding: false, label: 'Avance' },
  { id: 'rembourse', numeric: true, disablePadding: false, label: 'Remboursement' },
  { id: 'etat', numeric: false, disablePadding: false, label: 'Etat' },
  { id: 'view', numeric: true, disablePadding: false, label: '' },
];

class EnhancedTableHead extends Component {
  createSortHandler = property => event => {
    this.props.onRequestSort(event, property);
  };

  render() {
    const { onSelectAllClick, order, orderBy, numSelected, rowCount } = this.props;

    return (
      <TableHead >
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
            Liste des dossiers
          </Typography>
        )}
      </div>
      <div className={classes.spacer} />
  
      
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
     minWidth: 300,
  },
  tableWrapper: {
    overflowX: 'auto',
  },
});

class Dossies extends React.Component {
  state = {
    order: 'asc',
    orderBy: 'calories',
    selected: [],
    data: [

    ],
  isLoad:true,
    page: 0,
    rowsPerPage: 5,
  };
  componentDidMount()
  {
    const { data } = this.props;
    console.log('Data props');
    console.log(data);
    if(data != undefined)
    {
       this.setState({data});this.setState({isLoad:false})
       console.log("dossiers");
       console.log(data);
       console.log("end dossiers");
    }
    else
    {
      axios.get('api/dossiers')
      .then(res => {
        console.log(res.data);
        this.setState({ data: res.data, isLoad:false });
      })
    }
    
  }
  handleRequestSort = (event, property) => {
    const orderBy = property;
    let order = 'desc';

    if (this.state.orderBy === property && this.state.order === 'desc') {
      order = 'asc';
    }

    this.setState({ order, orderBy });
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

  /*formatDateDossier = (DateDossier) => {
    var d = new Date(DateDossier),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;

    return [day, month, year].join('-');
 };*/
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

  handleChangePage = (event, page) => {
    this.setState({ page });
  };

  handleChangeRowsPerPage = event => {
    this.setState({ rowsPerPage: event.target.value });
  };

  isSelected = id => this.state.selected.indexOf(id) !== -1;

  render() {
    const { classes } = this.props;
    const { data, order, orderBy, selected, rowsPerPage, page,isLoad } = this.state;
    const emptyRows = rowsPerPage - Math.min(rowsPerPage, data.length - page * rowsPerPage);

    return (
      <Paper className={classes.root}>
      {isLoad && 
      <LinearProgress />
      }
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
              {this.state.data.length > 0 && stableSort(data, getSorting(order, orderBy))
                .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                .map(n => {
                  const isSelected = this.isSelected(n.referance);
                  return (
                    <TableRow
                      hover
                      onClick={event => this.handleClick(event, n.referance)}
                      role="checkbox"
                      aria-checked={isSelected}
                      tabIndex={-1}
                      key={n.referance}
                      selected={isSelected}
                    >
                      <TableCell padding="checkbox">
                        <Checkbox checked={isSelected} />
                      </TableCell>

                      <TableCell align="right">
                        <Link style={{ color: '#A13537' }} to ={{pathname: '/dossiers/' + n.referance}}>{n.referance}
                        </Link>
                      </TableCell>
                      <TableCell align="right">
                      <Link  to={{pathname:'/employees/' + (n.employee != null ? n.employee.employeematricule : n.employeematricule)}}>
                        {n.employee != null ?
                           n.employee.matricule + ' : ' + n.employee.last_name + ' '  + n.employee.first_name 
                           : n.employeematricule}
                      </Link></TableCell>
                      <TableCell align="right">{this.formatDate(n.date)}</TableCell>
                     
                      <TableCell align="right">{n.montant}</TableCell>
                      <TableCell align="right">{n.avance}</TableCell>
                      <TableCell align="right">{n.rembourse}</TableCell>
                      <TableCell align="right">{n.etat}</TableCell>

                      <TableCell align="left">
                        <Link to={{pathname:'/dossiers/'+n.referance}}>
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

Dossies.propTypes = {
  classes: PropTypes.object.isRequired,
};

export default withStyles(styles)(Dossies);
