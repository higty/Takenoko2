import { $ } from "../../../HigLabo/HtmlElementQuery.js";
import { HttpClient } from "../../../HigLabo/HttpClient.js";
import { TinyMceTextBox } from "../../../HigLabo/TinyMceTextBox.js";
class Add {
    constructor() {
        this.textBox = new TinyMceTextBox();
    }
    initialize() {
        document.querySelector("[name='SaveButton']").addEventListener("click", this.saveButton_Click.bind(this));
        this.textBox.config.language = "ja";
        this.textBox.imageUploadUrlPath = "/api/file/upload";
        this.textBox.fileUploadUrlPath = "/api/file/upload";
        this.textBox.initialize($("input[name='BodyText']").getFirstElement());
    }
    saveButton_Click(target, e) {
        const p = {
            Title: $("input[name='Title']").getValue(),
            BodyText: this.textBox.getInnerHtml(),
        };
        HttpClient.postJson("/api/blog/entry/add", p, this.saveCallback.bind(this));
    }
    saveCallback(response) {
        location.href = "/blog/entry/list";
    }
}
const page = new Add();
page.initialize();
window["currentPage"] = page;
//# sourceMappingURL=add.js.map