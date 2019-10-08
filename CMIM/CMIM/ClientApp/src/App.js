import React, { Component } from 'react';
import MiniDrawer from './components/MenuSide/Menu';
import AuthHelperMethods from './components/AuthHelperMethods';
import withAuth from './components/withAuth.js';
import { withRouter } from "react-router-dom";

class App extends Component {
  displayName = App.name
  Auth = new AuthHelperMethods();

  _handleLogout = () => {this.Auth.logout();this.props.history.replace('/SignIn');}

  render() {
      return (
          <div>
              <MiniDrawer  />
            {/* <EnfantForm/> */}
          </div>
    );
  }
}

export default withRouter(App);
