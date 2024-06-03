import React, { Fragment, useState } from "react"
import { adminSidebar } from "../../utils/constants"
import { NavLink, Link, useNavigate } from "react-router-dom"
import clsx from "clsx"
import { AiOutlineCaretDown, AiOutlineCaretRight } from "react-icons/ai"
import { RiShareForwardLine } from "react-icons/ri"
import withBaseComponent from "hocs/withBaseComponent"

const activedStyle =
    "px-4 py-2 flex items-center gap-2  bg-blue-500 text-gray-100"
const notActivedStyle = "px-4 py-2 flex items-center gap-2  hover:bg-blue-100"

const AdminSidebar = () => {
    const navigate = useNavigate()
    const [actived, setActived] = useState([])
    const handleShowTabs = (tabID) => {
        if (actived.some((el) => el === tabID))
            setActived((prev) => prev.filter((el) => el !== tabID))
        else setActived((prev) => [...prev, tabID])
    }

    return (
        <div className=" bg-white h-full py-4">
            <Link
                to={"/"}
                className="flex flex-col justify-center items-center p-4 gap-2"
            >
                <small>Admin Workspace</small>
            </Link>
        </div>
    )
}

export default AdminSidebar
