import React from 'react';
import { Route, Routes } from "react-router-dom"
import path from "./utils/path"
import {
  AdminLayout,
} from "./pages/admin/AdminLayout.js"
import './App.css';

function App() {
  return (
    <div className="text-[30px]">
    <Routes>
    <Route path={path.ADMIN} element={<AdminLayout />}>
    
    </Route>
    </Routes>
    </div>
  );
}

export default App;
