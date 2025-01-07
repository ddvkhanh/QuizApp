import NavBar from "components/NavBar";
import type { Metadata } from "next";
import "styles/globals.css";


export const metadata: Metadata = {
  title: "QuizApp",
  description: "QuizApp by Kathy Dang",
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang="en">
      <body>
        <NavBar />
        <main>{children}</main>
      </body>
    </html>          
  );
}
