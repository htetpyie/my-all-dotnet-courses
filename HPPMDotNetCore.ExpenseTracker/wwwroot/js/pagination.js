//#region Pagination

//pageSetting = {pageNo,pageSize,totalPageNo,searchValue}

//##########require this at the page for pagination buttons########
// <nav aria-label="...">
//     <ul className="pagination" id="pagination"></ul>
// </nav>

function drawPagination(pageSetting) {

    let ul = $('#pagination');
    $(ul).empty();

    addPrevious(ul, pageSetting);
    addPages(ul, pageSetting);
    addNext(ul, pageSetting);

}

function addPrevious(ul, pageSetting) {
    pageSetting = JSON.parse(decodeURIComponent(pageSetting))
    let pageNo = pageSetting.pageNo;
    let pageSize = pageSetting.pageSize;
    let searchValue = pageSetting.searchValue;

    let previous = (pageNo > 1) ?
        `<li class="page-item">
                    <a class="page-link" href="javascript:" onclick="loadTable(${pageNo - 1},${pageSize}, ${searchValue})" tabindex="-1">Previous</a>
                </li>`
        :
        `<li class="page-item disabled">
                     <a class="page-link" href="#" tabindex="-1">Previous</a>
                 </li>`;
    $(ul).append(previous);
}

function addNext(ul, pageSetting) {
    pageSetting = JSON.parse(decodeURIComponent(pageSetting))
    let pageNo = pageSetting.pageNo;
    let pageSize = pageSetting.pageSize;
    let searchValue = pageSetting.searchValue;
    let totalPageNo = pageSetting.totalPageNo;

    let next = pageNo < totalPageNo ?
        `<li class="page-item">
                     <a class="page-link" href="javascript:" onclick="loadTable(${pageNo + 1},${pageSize}, ${searchValue})">Next</a>
                 </li>` :
        `<li class="page-item disabled">
                      <a class="page-link" href="#" tabindex="-1">Next</a>
                 </li>`;
    $(ul).append(next);
}

function addPages(ul, pageSetting) {
    pageSetting = JSON.parse(decodeURIComponent(pageSetting))
    let pageNo = pageSetting.pageNo;
    let pageSize = pageSetting.pageSize;
    let searchValue = pageSetting.searchValue;
    let totalPageNo = pageSetting.totalPageNo;

    if (totalPageNo < 9) {
        for (let i = 1; i <= totalPageNo; i++) {
            let li = (i === pageNo) ?
                `<li class="page-item active">
                             <a class="page-link" href="#">
                                 ${i}
                             </a>
                         </li>` :
                ` <li class="page-item"><a class="page-link" onclick="loadTable(${i},${pageSize},${searchValue})" href="javascript:">${i}</a></li>`;
            $(ul).append(li);
        }
    } else {
        if (pageNo <= 3 || pageNo >= totalPageNo - 2) {
            for (let i = 1; i <= 4; i++) {
                let li = (i === pageNo) ?
                    ` <li class="page-item active">
                                 <a class="page-link" href="#">${i}</a>
                             </li>` :
                    `<li class="page-item"><a class="page-link" onclick="loadTable(${i}, ${pageSize},${searchValue})" href="javascript:">${i}</a></li>`;
                $(ul).append(li);
            }
            $(ul).append(`<li class="page-item"><a class="page-link" href="#">...</a></li>`);
            for (let i = totalPageNo - 3; i <= totalPageNo; i++) {
                let li = (i === pageNo) ?
                    ` <li class="page-item active">
                                 <a class="page-link" href="#">${i}</a>
                             </li>` :
                    `<li class="page-item"><a class="page-link" onclick="loadTable(${i}, ${pageSize}, ${searchValue})" href="javascript:">${i}</a></li>`;
                $(ul).append(li);
            }
        } else {
            let first = `<li class="page-item"><a class="page-link" href="javascript:" onClick="loadTable(1,${pageSize}, ${searchValue})">1</a></li>`;
            $(ul).append(first);
            $(ul).append(`<li class="page-item"><a class="page-link" href="#">...</a></li>`);

            for (let i = pageNo - 2; i <= pageNo + 2; i++) {
                let li = (i === pageNo) ?
                    ` <li class="page-item active">
                                  <a class="page-link" href="#">${i}</a>
                              </li>` :
                    `<li class="page-item"><a class="page-link" onclick="loadTable(${i}, ${pageSize}, ${searchValue})" href="javascript:">${i}</a></li>`;
                $(ul).append(li);
            }

            $(ul).append(`<li class="page-item"><a class="page-link" href="#">...</a></li>`);
            let last = `<li class="page-item"><a class="page-link" href="javascript:" onClick="loadTable(${totalPageNo},${pageSize}, ${searchValue})">${totalPageNo}</a></li>`;
            $(ul).append(last);
        }
    }
}

//#endregion   