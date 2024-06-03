import React from 'react'
import InputForm from "../../components/inputs/InputForm"
import Pagination from "../../components/pagination/Pagination"
import { useForm } from "react-hook-form"

const ManageProduct = () => {
  const {
    register,
    formState: { errors },
    watch,
  } = useForm()
  return (
    <div className="w-full flex flex-col gap-4 relative">
      <div className="h-[69px] w-full"></div>
      <div className="p-4 border-b w-full bg-gray-100 flex justify-between items-center fixed top-0">
        <h1 className="text-3xl font-bold tracking-tight">Manage products</h1>
      </div>
      <div className="flex justify-end items-center px-4">
        <form className="w-[45%]">
          <InputForm
            id="q"
            register={register}
            errors={errors}
            fullWidth
            placeholder="Search products by title, description,..."
          />
        </form>
      </div>
      <table className="table-auto">
        <thead>
          <tr className="border bg-sky-900 text-white border-white">
            <th className="text-center py-2">ID</th>
            <th className="text-center py-2">Name Product</th>
            <th className="text-center py-2">Category Name</th>
            <th className="text-center py-2">Price</th>
            <th className="text-center py-2">Image</th>
            <th className="text-center py-2">Description</th>
            <th className="text-center py-2">Created Date</th>
            <th className="text-center py-2">Update Date</th>
            <th className="text-center py-2">Actions</th>
          </tr>
        </thead>
        <tbody>
          <tr className="border-b" >
            <td className="text-center py-2">
              01
            </td>
            <td className="text-center py-2">Acer swift 3</td>
            <td className="text-center py-2">Laptop</td>
            <td className="text-center py-2">1250</td>
            <td className="text-center py-2">
              <img
                src='https://th.bing.com/th/id/R.4f89820d7c9c3a2cf40641a26d10202b?rik=ihEhamuSoua%2fKw&pid=ImgRaw&r=0'
                alt="thumb"
                className="w-12 h-12 object-cover"
              />
            </td>
            <td className="text-center py-2">The techGuru UltraBook 15 </td>
            <td className="text-center py-2">26-05-2024</td>
            <td className="text-center py-2">01-01-2024</td>
            <td className="text-center py-2">Delete</td>
          </tr>
        </tbody>
      </table>
      <div className="w-full flex justify-end my-8">
        <Pagination totalCount={100} />
      </div>
    </div>
  )
}

export default ManageProduct
