import React from 'react'
import InputForm from "../../components/inputs/InputForm"
import Pagination from "../../components/pagination/Pagination"
import { useForm } from "react-hook-form"

const ManageUser = () => {
  const {
    register,
    formState: { errors },
    watch,
  } = useForm()
  return (
    <div className="w-full flex flex-col gap-4 relative">
      <div className="h-[69px] w-full"></div>
      <div className="p-4 border-b w-full bg-gray-100 flex justify-between items-center fixed top-0">
        <h1 className="text-3xl font-bold tracking-tight">Manage users</h1>
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
            <th className="text-center py-2">Full Name</th>
            <th className="text-center py-2">Phone Number</th>
            <th className="text-center py-2">Email</th>
            <th className="text-center py-2">Address</th>
            <th className="text-center py-2">Image</th>
            <th className="text-center py-2">Dob</th>
            <th className="text-center py-2">Gender</th>
            <th className="text-center py-2">Actions</th>
          </tr>
        </thead>
        <tbody>
          <tr className="border-b" >
            <td className="text-center py-2">
              01
            </td>
            <td className="text-center py-2">Trần Quang Đạt</td>
            <td className="text-center py-2">0945354040</td>
            <td className="text-center py-2">dattq123@gmail.com</td>
            <td className="text-center py-2">Binh Tân, Tp.HCM </td>
            <td className="text-center py-2">
              <img
                src='https://th.bing.com/th/id/OIP.IGNf7GuQaCqz_RPq5wCkPgAAAA?rs=1&pid=ImgDetMain'
                alt="thumb"
                className="w-12 h-12 object-cover"
              />
            </td>
            <td className="text-center py-2">25-09-2001</td>
            <td className="text-center py-2">Male</td>
          </tr>
        </tbody>
      </table>
      <div className="w-full flex justify-end my-8">
        <Pagination totalCount={100} />
      </div>
    </div>
  )
}

export default ManageUser
