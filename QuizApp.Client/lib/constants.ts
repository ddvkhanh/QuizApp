import routes from "./routes";

export const TIME_LIMIT = 30; //in seconds

interface NavItem {
    label: string;
    href: string;
  }

export const NAV_ITEMS: Array<NavItem> = [
    {
      label: "Home",
      href: routes.home,
    },
    {
      label: "Take Quiz",
      href: routes.quiz.base,
    },
    {
      label: "Manage Question",
      href: routes.questions.base,
    },
    {
      label: "View Attempts",
      href: routes.attempts.base,
    },
  ];