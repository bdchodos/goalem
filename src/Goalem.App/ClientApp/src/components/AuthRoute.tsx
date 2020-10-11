import React, { Component } from 'react'
import {
  Route,
  Redirect,
  RouteProps,
} from "react-router";
import { connect } from 'react-redux'
import { useAuth0 } from '@auth0/auth0-react'

const AuthRoute = (routeProps: RouteProps) => {
  const isAuthenticated = useAuth0().isAuthenticated;

  if (isAuthenticated) {
    return <Route {...routeProps} />
  }

  return <Redirect to='/login' />;
};

export default connect()(AuthRoute);

