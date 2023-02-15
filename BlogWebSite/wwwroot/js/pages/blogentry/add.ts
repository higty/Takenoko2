import { HtmlElementQuery, $ } from "../../../HigLabo/HtmlElementQuery.js";
import { HttpClient, HttpResponse } from "../../../HigLabo/HttpClient.js";
import { TinyMceTextBox } from "../../../HigLabo/TinyMceTextBox.js";

class Add {
    public textBox = new TinyMceTextBox();

    public initialize() {
        document.querySelector("[name='SaveButton']").addEventListener("click"
            , this.saveButton_Click.bind(this));

        this.textBox.config.language = "ja";
        this.textBox.imageUploadUrlPath = "/api/file/upload";
        this.textBox.fileUploadUrlPath = "/api/file/upload";
        this.textBox.initialize($("input[name='BodyText']").getFirstElement());
    }
    private saveButton_Click(target: Element, e: Event) {
        const p = {
            Title: $("input[name='Title']").getValue(),
            BodyText: this.textBox.getInnerHtml(),
        };
        HttpClient.postJson("/api/blog/entry/add", p, this.saveCallback.bind(this));
    }
    private saveCallback(response: HttpResponse) {
        location.href = "/blog/entry/list";
    }
}

const page = new Add();
page.initialize();
window["currentPage"] = page;
