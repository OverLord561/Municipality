import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import Home from './components/Home';
import FetchData from './components/FetchData';
import Counter from './components/Counter';
import SignUp from './components/SignUp/SignUp';
import SignIn from './components/SignIn/SignIn';
import Incidents from './components/Incidents/Incidents';

export const routes = <Layout>
    <Route exact path='/' component={ Home } />
    <Route path='/counter' component={ Counter } />
    <Route path='/fetchdata/:startDateIndex?' component={FetchData} />
    <Route path='/sign-up' component={SignUp} />
    <Route path='/sign-in' component={SignIn} />
    <Route path='/incidents' component={Incidents} />
</Layout>;
