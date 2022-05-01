import React from 'react'
import './App.css'
import { Route, Routes, Navigate } from 'react-router-dom'
import GamesManager from './pages/GamesManager'
import Layout from './components/Layout'

import './assets/styles/app.scss'

function App() {
  return (
    <div className="App">
      <Layout>
        <Routes>
          <Route path="/" element={<Navigate replace to="/games" />} />
          <Route path="/games/*" element={<GamesManager />} />
          <Route path="*" element={<Navigate replace to="/games" />} />
        </Routes>
      </Layout>
    </div>
  )
}

export default App
