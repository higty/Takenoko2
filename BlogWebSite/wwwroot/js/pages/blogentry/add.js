import { $ } from "../../../HigLabo/HtmlElementQuery.js";
import { HttpClient } from "../../../HigLabo/HttpClient.js";
class Add {
    initialize() {
        document.querySelector("[name='SaveButton']").addEventListener("click", this.saveButton_Click.bind(this));
    }
    saveButton_Click(target, e) {
        const p = {
            Title: $("input[name='Title']").getValue(),
            BodyText: $("input[name='BodyText']").getValue(),
        };
        HttpClient.postJson("/api/blog/entry/add", p, this.saveCallback.bind(this));
    }
    saveCallback(response) {
        alert("Saved!");
        location.href = "/blog/entry/list";
    }
}
const page = new Add();
page.initialize();
//# sourceMappingURL=add.js.map