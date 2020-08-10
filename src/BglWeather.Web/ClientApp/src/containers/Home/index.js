import React, { Component } from 'react';
import SpaWebContent from '../../components/SpaContent';
import Footer from '../../components/Common/Footer';
import Header from '../../components/Common/Header';

export default class Home extends Component {
    render() {
        return (
            <div>
                <Header />
                <SpaWebContent />
                <Footer />
            </div>
        );
    }
}
