import * as React from "react";
import { Route, RouteProps } from "react-router";
import Layout from "./components/Layout";
import Home from "./components/Home";
import FetchData from "./components/FetchData";
import Profile from "./components/profile/Profile";
import Login from "./components/Login";
import { useAuth0, withAuthenticationRequired } from "@auth0/auth0-react";

import "./custom.css";

const ProtectedRoute: React.FC<RouteProps> = (props) => (
  <Route
    {...props}
    component={props.component && withAuthenticationRequired(props.component)}
  />
);

export default () => {
  const { isLoading, isAuthenticated } = useAuth0();

  if (isLoading) {
    return <div>Loading...</div>;
  }

  const layoutProps = {
    isAuthenticated: isAuthenticated,
  };

  return (
    <Layout {...layoutProps}>
      <Route path="/Login" component={Login} />
      <ProtectedRoute exact path="/" component={Home} />
      <ProtectedRoute path="/profile" component={Profile} />
      <ProtectedRoute
        path="/fetch-data/:startDateIndex?"
        component={FetchData}
      />
    </Layout>
  );
};
