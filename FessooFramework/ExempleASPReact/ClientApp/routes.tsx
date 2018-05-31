import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import Home from './components/Home';
import FetchData from './components/FetchData';
import Example from './components/Example';
import Counter from './components/Counter';
import Auth from './components/Auth';
import ExampleModelData from './components/ExampleModelData';

export const routes = <Layout>
    <Route exact path='/' component={ Home } />
    <Route path='/Auth' component={Auth} />
    <Route path='/counter' component={ Counter } />
    <Route path='/fetchdata/:startDateIndex?' component={ FetchData } />
    <Route path='/example' component={Example} />
    <Route path='/ExampleModelData' component={ExampleModelData} />
</Layout>;
