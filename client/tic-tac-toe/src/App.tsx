import React from 'react'
import './App.css'
import { Route, Routes } from 'react-router-dom'
import GamesManager from './pages/GamesManager'
import Layout from './components/Layout'

import './assets/styles/app.scss'
import {NotificationsContextProvider} from "./contexts/notifications";
import Notifications from "./containers/Notifications";

function App() {
  return (
    <div className="App">
      <Layout>
          <NotificationsContextProvider>
              <Notifications>
                <Routes>
                  <Route path="*" element={<GamesManager />} />
                </Routes>
              </Notifications>
          </NotificationsContextProvider>
      </Layout>
    </div>
  )
}

export default App
