

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

export const generateRange = (start, end) => {
    const length = end + 1 - start
    return Array.from({ length }, (_, index) => start + index)
  }
  export function getBase64(file) {
    if (!file) return ""
    return new Promise((resolve, reject) => {
      const reader = new FileReader()
      reader.readAsDataURL(file)
      reader.onload = () => resolve(reader.result)
      reader.onerror = (error) => reject(error)
    })
  }