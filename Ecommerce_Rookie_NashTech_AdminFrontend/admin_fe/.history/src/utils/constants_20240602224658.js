import path from "./path"
import icons from "./icons"

const { AiOutlineDashboard, MdGroups, TbBrandProducthunt, RiBillLine } = icons
export const adminSidebar = [
    {
      id: 1,
      type: "SINGLE",
      text: "Dashboard",
      path: `/${path.ADMIN}/${path.DASHBOARD}`,
      icon: <AiOutlineDashboard size={20} />,
    },
    {
      id: 2,
      type: "SINGLE",
      text: "Manage users",
      path: `/${path.ADMIN}/${path.MANAGE_USER}`,
      icon: <MdGroups size={20} />,
    },
    {
      id: 3,
      type: "PARENT",
      text: "Products",
      icon: <TbBrandProducthunt size={20} />,
      submenu: [
        {
          text: "Create product",
          path: `/${path.ADMIN}/${path.CREATE_PRODUCTS}`,
        },
        {
          text: "Manage products",
          path: `/${path.ADMIN}/${path.MANAGE_PRODUCTS}`,
        },
      ],
    },
    {
      id: 4,
      type: "PARENT",
      text: "Categories",
      icon: <RiBillLine size={20} />,
      submenu: [
        {
          text: "Create category",
          path: `/${path.ADMIN}/${path.CREATE_CATEGORY}`,
        },
        {
          text: "Manage categories",
          path: `/${path.ADMIN}/${path.MANAGE_CATEGORIES}`,
        },
      ],
    },
    {
      id: 5,
      type: "SINGLE",
      text: "Logout",
      path: `/${path.LOGIN}`,
      icon: <AiOutlineDashboard size={20} />,
    },
  ]