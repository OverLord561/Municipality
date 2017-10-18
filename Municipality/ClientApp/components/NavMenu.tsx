import * as React from 'react';
import { NavLink, Link } from 'react-router-dom';

export class NavMenu extends React.Component<{}, {}> {
    public render() {
        return <div className='main-nav'>
                <div className='navbar navbar-inverse'>
                <div className='navbar-header'>
                    <button type='button' className='navbar-toggle' data-toggle='collapse' data-target='.navbar-collapse'>
                        <span className='sr-only'>Toggle navigation</span>
                        <span className='icon-bar'></span>
                        <span className='icon-bar'></span>
                        <span className='icon-bar'></span>
                    </button>
                    <Link className='navbar-brand' to={ '/' }>Municipality</Link>
                </div>
                <div className='clearfix'></div>
                <div className='navbar-collapse collapse'>
                    <ul className='nav navbar-nav'>
                        <li>
                            <NavLink exact to={ '/' } activeClassName='active'>
                                <span className='glyphicon glyphicon-home'></span> Home
                            </NavLink>
                        </li>
                        <li>
                            <NavLink to={ '/counter' } activeClassName='active'>
                                <span className='glyphicon glyphicon-education'></span> Counter
                            </NavLink>
                        </li>

                        <li>
                            <NavLink to={'/incidents'} activeClassName='active'>
                                <span className='glyphicon glyphicon-warning-sign'></span> Incidents
                            </NavLink>
                        </li>

                        <li>
                            <NavLink to={ '/fetchdata' } activeClassName='active'>
                                <span className='glyphicon glyphicon-th-list'></span> Fetch data
                            </NavLink>
                        </li>
                        <li>
                            <NavLink to={'/sign-up'} activeClassName='active'>
                                <span className='glyphicon glyphicon-share-alt'></span> Sign Up
                            </NavLink>
                        </li>

                        <li>
                            <NavLink to={'/sign-in'} activeClassName='active'>
                                <span className='glyphicon glyphicon-home'></span> Sign In
                            </NavLink>
                        </li>
                    </ul>
                </div>
            </div>
        </div>;
    }
}
