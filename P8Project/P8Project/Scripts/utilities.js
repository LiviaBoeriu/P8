function getAge(date) {
    console.log(date)
    return new Date().getFullYear() - parseInt(date.substring(0, 4))
}