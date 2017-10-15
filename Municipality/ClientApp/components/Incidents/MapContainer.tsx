import * as React from "react";
import { Link, RouteComponentProps } from 'react-router-dom';
import { connect } from 'react-redux';

import GoogleMap  from 'google-map-react';

const AnyReactComponent = ({ text }: any) => <div className="elem">{text}</div>;

const Dnipropetrovsk: any = { lat: 48.460861, lng: 35.056737 };

export default class MapContainer extends React.Component<any, any> {
    constructor(props: any) {

        super(props);
        this.state = {
            center: Dnipropetrovsk,
            zoom: 10
        };
    }
    render() {

        const K_MARGIN_TOP = 30;
        const K_MARGIN_RIGHT = 30;
        const K_MARGIN_BOTTOM = 30;
        const K_MARGIN_LEFT = 30;
        
        return (
            <GoogleMap 
                bootstrapURLKeys={{
                    key: "AIzaSyAxVVyh7rh6kKCYhxyWZSY_xkDNZ4YIK3k",
                    language: 'uk'                   
                }}
               
                defaultCenter={this.state.center}
                defaultZoom={this.state.zoom}
                margin={[K_MARGIN_TOP, K_MARGIN_RIGHT, K_MARGIN_BOTTOM, K_MARGIN_LEFT]}
                
                
            >
                <AnyReactComponent
                    lat={48.462592}
                    lng={35.049769}                    
                    text={'Упало дерево'}
                />
            </GoogleMap >
        );
    }
}
