import React from 'react'
import { InputForm } from "../../components/inputs/InputForm"

const ManageProduct = () => {
  return (
    <div className="w-full flex flex-col gap-4 relative">
      <div className="h-[69px] w-full"></div>
      <div className="p-4 border-b w-full bg-gray-100 flex justify-between items-center fixed top-0">
        <h1 className="text-3xl font-bold tracking-tight">Manage products</h1>
      </div>
    </div>
  )
}

export default ManageProduct
