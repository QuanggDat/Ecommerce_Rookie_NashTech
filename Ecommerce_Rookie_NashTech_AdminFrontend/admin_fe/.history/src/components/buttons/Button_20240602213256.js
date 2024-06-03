import React, { memo }  from 'react'

const Button = ({ name, handleOnclick, syle }) => {
    return (
        <div>
            <button
                type='button'
                className="block w-full max-w-xs mx-auto bg-indigo-500 hover:bg-indigo-700 focus:bg-indigo-700 text-white rounded-lg px-3 py-3 font-semibold"
                onClick={() => { handleOnclick && handleOnclick() }}
            >
                <span>{name} </span>
            </button>
        </div>
    )
}

export default memo(Button)
