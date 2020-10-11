import * as React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import Counter from './components/Counter';
import FetchData from './components/FetchData';
import AuthRoute from './components/AuthRoute';
import Login from './components/Login';
import { useAuth0 } from '@auth0/auth0-react'

import './custom.css'

export default () => {
  const { isLoading, isAuthenticated } = useAuth0();

  if (isLoading) {
    return <div>Loading...</div>;
  }

  const layoutProps = {
    isAuthenticated: isAuthenticated
  };

  return (
    <Layout {...layoutProps}>
      <Route path='/Login' component={Login} />
      <AuthRoute exact path='/' component={Home} />
      <AuthRoute path='/counter' component={Counter} />
      <AuthRoute path='/fetch-data/:startDateIndex?' component={FetchData} />
    </Layout>
  );
}