import React from 'react'

const InputField = ({ value, setValue, nameKey, type, invalidFields, setInvalidFields, placeholder, icon }) => {
    const iconClass = icon ? `mdi mdi-${icon}-outline text-gray-400 text-lg` : '';
    return (
        <div>
            <div className='flex w-full'>
                <div className="w-10 z-10 pl-1 text-center pointer-events-none flex items-center justify-center"><i className={iconClass}></i></div>
                <input
                    type={type || "text"}
                    className="w-full -ml-10 pl-10 pr-3 py-2 rounded-lg border-2 border-gray-200 outline-none focus:border-indigo-500"
                    value={value}
                    placeholder={placeholder}
                    onChange={e => setValue(prev => ({ ...prev, [nameKey]: e.target.value }))}
                    onFocus={() => setInvalidFields && setInvalidFields([])}
                />
            </div>
            {invalidFields?.some(el => el.name === nameKey) && <small className='text-red-400 italic'>{invalidFields.find(el => el.name === nameKey)?.mes}</small>}
        </div>
    )
}

export default InputField
