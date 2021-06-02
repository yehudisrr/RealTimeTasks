import React, { Component } from 'react';
import { Route } from 'react-router-dom';
import Layout from './components/Layout';
import Home from './Pages/Home';
import Signup from './Pages/Signup';
import Login from './Pages/Login';
import AuthContextComponent from './AuthContext';
import PrivateRoute from './PrivateRoute';
import Logout from './Pages/Logout';

export default class App extends Component {
    displayName = App.name

    render() {
        return (
            <AuthContextComponent>
                <Layout>
                    <PrivateRoute exact path='/' component={Home} />
                    <Route exact path='/signup' component={Signup} />
                    <Route exact path='/login' component={Login} />
                    <Route exact path='/logout' component={Logout} />
                </Layout>
            </AuthContextComponent>
        );
    }
}

                
                   