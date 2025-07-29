function calculate(){
    const table = document.getElementById("records");
    const resultArea = document.getElementById("result");
    let result = 0;

    for (let i = 1; i < table.rows.length; i++) {
        result = result + +table.rows[i].cells[1].innerHTML;
    }
        resultArea.append(`${result}`)
}