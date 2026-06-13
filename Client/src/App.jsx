// root compoent of the react 
import { Link, Navigate, NavLink, Route, Routes } from 'react-router-dom'
import HomePage from './pages/HomePage';

function Navbar() { return (
    <nav className="navbar">
    <Link to="/home" className="toHome">
        Coding Practice
    </Link>

    </nav>
);}

export default function App() { return (
    <>
        <Navbar /> 

        <main className="canvas">
            <Routes>
                <Route path="/home" element={<  HomePage />} />
            </Routes>
        </main>
    </>
);}