import { HtmlElementQuery, $ } from "../../../HigLabo/HtmlElementQuery.js";
import { HttpClient, HttpResponse } from "../../../HigLabo/HttpClient.js";

class Add {
    public initialize() {
        document.querySelector("[name='SaveButton']").addEventListener("click"
            , this.saveButton_Click.bind(this));
    }
    private saveButton_Click(target: Element, e: Event) {
        const p = {
            Title: $("input[name='Title']").getValue(),
            BodyText: $("input[name='BodyText']").getValue(),
        };
        HttpClient.postJson("/api/blog/entry/add", p, this.saveCallback.bind(this));
    }
    private saveCallback(response: HttpResponse) {
        alert("Saved!");
        location.href = "/blog/entry/list";
    }
}

const page = new Add();
page.initialize();