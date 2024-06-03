

export const validate = (payload, setInvalidFields) => {
    let invalids = 0
    const formatPayload = Object.entries(payload)
    for (let arr of formatPayload) {
        if (arr[1].trim() === "") {
            invalids++
            setInvalidFields((prev) => [
                ...prev,
                { name: arr[0], mes: "Require this field." },
            ])
        }
    }
    return invalids
}