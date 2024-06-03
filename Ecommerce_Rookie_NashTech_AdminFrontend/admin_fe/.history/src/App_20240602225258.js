import React from 'react';
import { Route, Routes, Navigate } from "react-router-dom"
import path from "./utils/path"
import {
  AdminLayout,
  Dashboard,
  ManageProduct,
  ManageCategory
} from "./pages/admin"
import { Login } from './pages/public';
import './App.css';

function App() {
  return (
    <div className="font-jp">
      <Routes>
        <Route path="/" element={<Navigate to={path.LOGIN} />} />
        <Route path={path.LOGIN} element={<Login />} />
        <Route path={path.ADMIN} element={<AdminLayout />}>
          <Route path={path.DASHBOARD} element={<Dashboard />} />
          <Route path={path.MANAGE_PRODUCTS} element={<ManageProduct />} />
          <Route path={path.MANAGE_CATEGORIES} element={<ManageCategory />} />
        </Route>
      </Routes>
    </div>
  );
}

export default App;
