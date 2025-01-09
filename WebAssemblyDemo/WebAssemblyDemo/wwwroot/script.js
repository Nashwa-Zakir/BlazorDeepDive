function showMessage(message) {

    const result = 2 * 20
    alert(message + "" + result)
}

function calculateAge(yearOfBirth) {
    const currentYear = new Date().getFullYear()
    return currentYear-yearOfBirth
}

function incrementcounter() {
    console.log("from javascript");
    DotNet.invokeMethodAsync("WebAssemblyDemo.Client", "IncrementCount")
}