import React from 'react';
import { Route, Routes } from "react-router-dom"
import path from "./utils/path"
import {
  AdminLayout,
  Dashboard
} from "./pages/admin/AdminLayout.js"
import './App.css';

function App() {
  return (
    <div className="text-[30px]">
      <Routes>
        <Route path={path.ADMIN} element={<AdminLayout />}>
          <Route path={path.DASHBOARD} element={<Dashboard />} />
        </Route>
      </Routes>
    </div>
  );
}

export default App;
