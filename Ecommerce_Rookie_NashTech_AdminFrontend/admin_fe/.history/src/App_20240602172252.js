import React from 'react';
import { Route, Routes, Navigate } from "react-router-dom"
import path from "./utils/path"
import {
  AdminLayout,
  Dashboard,
  LoginScreen,
  ManageProduct
} from "./pages/admin"
import './App.css';

function App() {
  return (
    <div className="text-[30px]">
      <Routes>
        <Route path="/" element={<Navigate to={path.LOGIN} />} />
        <Route path={path.LOGIN} element={<LoginScreen />} />
        <Route path={path.ADMIN} element={<AdminLayout />}>
          <Route path={path.DASHBOARD} element={<Dashboard />} />
          <Route path={path.MANAGE_PRODUCTS} element={<ManageProduct />} />
        </Route>
      </Routes>
    </div>
  );
}

export default App;
