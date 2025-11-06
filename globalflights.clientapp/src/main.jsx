import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.jsx'
import FlightStream from './FlightStream'

createRoot(document.getElementById('root')).render(
  <StrictMode>
        <FlightStream />
  </StrictMode>,
)
