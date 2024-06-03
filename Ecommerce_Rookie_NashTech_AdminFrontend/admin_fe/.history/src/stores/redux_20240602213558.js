import { configureStore } from '@reduxjs/toolkit';
import userSlice from "./user/userSlice"
import storage from 'redux-persist/lib/storage';
import { persistReducer, persistStore } from "redux-persist"

const commonConfig = {
  storage,
}
const userConfig = {
  ...commonConfig,
  whitelist: ["isLoggedIn", "token"],
  key: "shop/user",
}
export const store = configureStore({
  reducer: {
    user: persistReducer(userConfig, userSlice),
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware({ serializableCheck: false }),
});

export const persistor = persistStore(store)