// root compoent of the react 
import { Link, Navigate, NavLink, Route, Routes } from 'react-router-dom';
import { useAuth0 } from '@auth0/auth0-react';
import HomePage from './pages/HomePage';
import ProblemsPage from './pages/ProblemsPage';
import SingleProblemPage from './pages/SingleProblemPage';

function RequireAuth({ children }) {
   const { isLoading, isAuthenticated, error, user, loginWithRedirect, logout } =
    useAuth0();

  if (isLoading) {
    return <div>Loading...</div>;
  }
  if (error) {
    return <div>Something went wrong... {error.message}</div>;
  }

  if (isAuthenticated) {
    return (
      <div>
        Hello {user.name}{' '}
        <button onClick={() => logout({ logoutParams: { returnTo: window.location.origin } })}>
          Log out
        </button>
      </div>
    );
  } else {
    return <button onClick={() => loginWithRedirect()}>Log in</button>;
  }
}

// TO do: add buttons Home, Practice, Rank, Profile
function Navbar() { return (
    <nav className="navbar">
    <Link to="/home" className="home"> Coding Practice == </Link>
    <Link to="/problems" className="problems"> Practice == </Link>
    <Link to="/rank" className="ranklist"> Rank == </Link>
    <Link to={{
        pathname: "/profile",
        search: "?sort=name",
        hash: "#results"
    }} className="profile"> Profile == </Link>
    </nav>
);}

export default function App() { 
    return (
        <>
            <Navbar /> 
            <main className="container">
                <Routes>
                    <Route path="/home" element={<  HomePage />} />
                    <Route path="/problems" element={< ProblemsPage />} />
                    <Route path="/rank" element={<h1>Rank</h1>} />
                    <Route path="/profile" element={
                        // <RequireAuth>
                            <h1>Profile</h1>
                        /* </RequireAuth> */
                    } />
                    <Route path="/problems/:id" element={<SingleProblemPage/>}/>
                    <Route path="*" element={<Navigate to="/home" />} />    
                </Routes>
            </main>
        </>
    );
}