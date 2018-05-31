import * as React from 'react';
import { Link, RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';
import { ApplicationState } from '../store';

export default class Counter extends React.Component<RouteComponentProps<{}>, {}> {
    public render() {
        return <div>
            <Link to="/dashboard" >
                <button>
                    <p>Sign In</p>
                </button>
            </Link>
            <form action="/">
                <input type="submit" value="Войти" />
            </form>
        </div>;
    }
}