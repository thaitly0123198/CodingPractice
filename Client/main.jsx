// react entry point
import React from 'react';
import { Auth0Provider } from '@auth0/auth0-react';
import { createRoot } from 'react-dom/client';
import { BrowserRouter } from 'react-router-dom';
import App from './src/App.jsx';
import './src/assets/styles/main.css';


createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    {/* <Auth0Provider
      domain={"dev-tvssvpkbc86ezaim.jp.auth0.com"}
      clientId={"jEX1nj62QYtVd4ugGCiC5GMpGZJukZ9q"}
      authorizationParams={{
        redirect_uri: window.location.origin,
      }}
    > */}
      <BrowserRouter>
          <App />
      </BrowserRouter>
    {/* </Auth0Provider> */}
  </React.StrictMode>,
)
