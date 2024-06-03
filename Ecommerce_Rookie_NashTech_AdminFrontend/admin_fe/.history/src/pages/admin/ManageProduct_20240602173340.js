import React from 'react'
import InputForm from "../../components/inputs/InputForm"
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
            <th className="text-center py-2">Order</th>
            <th className="text-center py-2">Thumb</th>
            <th className="text-center py-2">Title</th>
            <th className="text-center py-2">Brand</th>
            <th className="text-center py-2">Category</th>
            <th className="text-center py-2">Price</th>
            <th className="text-center py-2">Quantity</th>
            <th className="text-center py-2">Sold</th>
            <th className="text-center py-2">Ratings</th>
            <th className="text-center py-2">Actions</th>
          </tr>
        </thead>
        <tbody>
          <tr className="border-b" >
            <td className="text-center py-2">
              01
            </td>
            <td className="text-center py-2">
              <img
                src='https://th.bing.com/th/id/OIF.IHFaCxQQ54StkxKZ3Iyp0Q?rs=1&pid=ImgDetMain'
                alt="thumb"
                className="w-12 h-12 object-cover"
              />
            </td>
            <td className="text-center py-2">1</td>
            <td className="text-center py-2">1</td>
            <td className="text-center py-2">1</td>
            <td className="text-center py-2">1</td>
            <td className="text-center py-2">1</td>
            <td className="text-center py-2">1</td>
            <td className="text-center py-2">1</td>
            <td className="text-center py-2">1</td>
            <td className="text-center py-2">1</td>
            <td className="text-center py-2">
              2/6/2024
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  )
}

export default ManageProduct
