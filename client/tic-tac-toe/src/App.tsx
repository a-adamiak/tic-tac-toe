import React from 'react';
import './App.css';
import {Route, Routes, Navigate } from 'react-router-dom';
import GamesManager from './containers/GamesManager';
import Layout from './components/Layout';

function App() {
  return (
    <div className="App">
        <Layout>
          <Routes>
              <Route path='/' element={<Navigate replace to='/games' />} />
              <Route path='/games/*' element={<GamesManager />} />
          </Routes>
        </Layout>
    </div>
  );
}

export default App;
