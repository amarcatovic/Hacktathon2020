import { environment } from "./../../../../environments/environment.prod";
import { IssueBaseService } from "./../../services/issue-base.service";
import { Component, OnInit } from "@angular/core";

@Component({
  selector: "app-issue-base",
  templateUrl: "./issue-base.component.html",
  styleUrls: ["./issue-base.component.scss"],
})
export class IssueBaseComponent implements OnInit {
  code: number = 0;

  issues: any[];
  issueSearch: string;
  commentToggle: boolean = false;
  questionToggle: boolean = false;
  issueId: number;
  questionTitle: string;

  questionQuestion: string;
  questionTitleCreate: string;

  commentAnswer: string;

  constructor(private issueBaseService: IssueBaseService) {}

  ngOnInit(): void {
    this.getAllIssues();

    this.issueBaseService.fetchAgain.subscribe((_) => {
      this.getAllIssues();
      this.questionTitle = "";
      this.questionQuestion = "";
      this.commentAnswer = "";
      this.questionTitleCreate = "";
      this.commentToggle = false;
      this.questionToggle = false;
    });
  }

  getAllIssues(): void {
    this.issueBaseService.getIssues().subscribe((data: any) => {
      this.issues = data;
      console.log(data);
    });
  }

  toggleQuestiion() {
    this.questionToggle = !this.questionToggle;
  }

  selectCodeFormat(num: number) {
    this.code = num;
  }

  toggleComment(id: number, title: string) {
    if (this.issueId === id) {
      this.commentToggle = false;
      this.issueId = -1;
    } else {
      this.commentToggle = true;
      this.issueId = id;
      this.questionTitle = title;
    }
  }

  otherCode() {
    var text =
      "<pre>" +
      (document.getElementById("comment-area") as HTMLTextAreaElement).value +
      "<pre>";
    var stringText = text.toString();
    var dom = document.createElement("div");
    dom.innerHTML = stringText;
    document.body.appendChild(dom);
  }

  // allIssues() {
  //   let service = new IssueBaseService;
  //   this.issues = service.getIssues();
  //   createDiv(this.issues);

  // };

  // searchResults() {
  //   let keyWord = (document.getElementById("search-field") as HTMLTextAreaElement).value;
  //   let service = new IssueBaseService;
  //   this.issues = service.getSearchResult();
  //   createDiv(this.issues);
  // };

  createComment() {
    createCommentAreaElement();
  }

  createQuestion() {
    createQuestionAreaElement();
  }

  addComment() {
    this.issueBaseService.addComment(
      this.issueId,
      this.commentAnswer,
      this.code
    );
  }

  addQuestion() {
    this.issueBaseService.createIssue(
      this.questionQuestion,
      this.questionTitleCreate,
      this.code
    );
  }

  formatCode(code) {
    console.log(code);
    let text = "<pre>" + code + "<pre>";
    console.log(text);
    return text;
  }

  formatHtml(html) {
    let tab = "\t";
    let result = "";
    let indent = "";

    html.split(/>\s*</).forEach(function (element) {
      if (element.match(/^\/\w/)) {
        indent = indent.substring(tab.length);
      }

      result += indent + "<" + element + ">\r\n";

      if (element.match(/^<?\w[^>]*[^\/]$/)) {
        indent += tab;
      }
    });

    return result.substring(1, result.length - 3);
  }
}

function createQuestionAreaElement() {
  //document.getElementsByClassName("question-area")[0].classList.toggle("display");
}

function createCommentAreaElement() {
  document
    .getElementsByClassName("comment-area")[0]
    .classList.toggle("display");
}

function formatCode(code) {
  let text = "<pre>" + code + "<pre>";
  return code.toString();
}

function formatHtml(html) {
  let tab = "\t";
  let result = "";
  let indent = "";

  html.split(/>\s*</).forEach(function (element) {
    if (element.match(/^\/\w/)) {
      indent = indent.substring(tab.length);
    }

    result += indent + "<" + element + ">\r\n";

    if (element.match(/^<?\w[^>]*[^\/]$/)) {
      indent += tab;
    }
  });

  return result.substring(1, result.length - 3);
}

function createDiv(issues) {
  document.getElementsByClassName("list-issue")[0].classList.add("display");
  let issueDiv = document.getElementsByClassName("list-issue");
  issueDiv[0].innerHTML = "";
  let listOfIssues = document.createElement("ul");
  listOfIssues.className = "issue-list";
  for (let i = 0; i < issues.length; i++) {
    let listElement = document.createElement("li");
    listElement.className = "issue";
    let title = document.createElement("h6");
    title.innerText = "Question: " + issues[i].title;
    title.className = "issue-title";
    listElement.append(title);

    let issueContent = issues[i].content;
    if (issues[i].code == 1) issueContent = formatHtml(issues[i].content);
    if (issues[i].code == 2) issueContent = formatCode(issues[i].content);
    let content = document.createElement("p");
    content.innerText = issueContent;
    content.className = "issue-content";
    listElement.append(content);

    //KOMENTARI
    let listOfComments = document.createElement("ul");
    listOfComments.className = "list-of-comments";
    for (let j = 0; j < issues[i].comments.length; j++) {
      let commentElement = document.createElement("li");
      let commentTitle = document.createElement("h6");
      commentTitle.innerText = "Answer: " + issues[i].title;
      commentElement.append(commentTitle);

      let issueContent = issues[i].content;
      if (issues[i].code == 1) issueContent = formatHtml(issues[i].content);
      if (issues[i].code == 2) issueContent = formatCode(issues[i].content);
      let commentContent = document.createElement("p");
      commentContent.innerText = issueContent;
      commentContent.className = "comment-content";
      commentElement.append(commentContent);

      listOfComments.appendChild(commentElement);
    }

    listElement.appendChild(listOfComments);
    let commentBtn = document.createElement("button");
    commentBtn.innerText = "COMMENT";
    commentBtn.className = "add-comment";
    commentBtn.addEventListener("click", createCommentAreaElement);
    commentBtn.name = issues[i].id;
    listElement.append(commentBtn);
    listOfIssues.appendChild(listElement);
  }
  issueDiv[0].append(listOfIssues);
}
