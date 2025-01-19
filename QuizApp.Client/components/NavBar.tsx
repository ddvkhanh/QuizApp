"use client"

import { useState } from "react";
import { Dialog, DialogPanel } from "@headlessui/react";
import { Bars3Icon, XMarkIcon, ArrowLeftEndOnRectangleIcon } from "@heroicons/react/24/outline";
import Link from "next/link";
import { NAV_ITEMS } from "lib/constants";

export default function NavBar() {
    const [mobileMenuOpen, setMobileMenuOpen] = useState(false);    

    return (
        <header className="absolute inset-x-0 sticky top-0 z-50 text-white">
            <nav
                aria-label="Global"
                className="flex items-center justify-between p-6 lg:px-8 bg-gray-800 "
            >
                <div className="flex lg:hidden">
                    <button
                        type="button"
                        onClick={() => setMobileMenuOpen(true)}
                        className="-m-2.5 inline-flex items-center justify-center rounded-md p-2.5"
                    >
                        <span className="sr-only">Open main menu</span>
                        <Bars3Icon aria-hidden="true" className="h-6 w-6" />
                    </button>
                </div>
                <div className="w-5/6 hidden lg:flex lg:gap-x-12 w-full">
                    {NAV_ITEMS.map((item) => (
                        <Link href={item.href}
                            key={item.label}
                            className="-mx-3 block text-white no-underline rounded-lg px-3 py-2 text-base font-semibold leading-7 hover:bg-gray-500"
                        >{item.label}</Link>
                    ))}                                        
                </div>
                <div className="w-1/6 hidden lg:flex justify-end">
                    <button className="-mx-3 flex block text-white text-base font-semibold no-underline rounded-lg p-3 bg-teal-600 hover:bg-teal-700">
                        <ArrowLeftEndOnRectangleIcon aria-hidden="true" className="h-6 w-6" />
                        <a href="/auth/login" className = "px-2">Log In</a>
                    </button>                   
                </div>
            </nav>
            <Dialog
                open={mobileMenuOpen}
                onClose={setMobileMenuOpen}
                className="lg:hidden"
            >
                <div className="fixed inset-0 z-50" />
                <DialogPanel className="fixed inset-y-0 right-0 z-50 w-full overflow-y-auto px-6 py-6 sm:max-w-sm sm:ring-1 sm:ring-gray-900/10 bg-white">
                    <div className="flex items-center justify-between">
                        <button
                            type="button"
                            onClick={() => setMobileMenuOpen(false)}
                            className="-m-2.5 rounded-md p-2.5 text-gray-700"
                        >
                            <span className="sr-only">Close menu</span>
                            <XMarkIcon aria-hidden="true" className="h-6 w-6" />
                        </button>
                    </div>
                    <div className="mt-6 flow-root">
                        <div className="-my-6 divide-y divide-gray-500/10">
                            <div className="space-y-2 py-6">
                                {NAV_ITEMS.map((item) => (
                                    <Link href={item.href}
                                        key={item.label}
                                        className="-mx-3 block text-black no-underline rounded-lg px-3 py-2 text-base font-semibold leading-7 hover:bg-slate-300"
                                    >{item.label}</Link>
                                ))}
                            </div>
                        </div>
                        <div className="mt-6 mx-3">
                            <button className="-mx-3 flex block text-white text-base font-semibold no-underline rounded-lg p-3 bg-teal-600 hover:bg-teal-700">
                                <ArrowLeftEndOnRectangleIcon aria-hidden="true" className="h-6 w-6" />
                                <a href="/auth/login" className = "px-2">Log In</a>
                            </button>  
                            <button className="-mx-3 flex block text-white text-base font-semibold no-underline rounded-lg p-3 bg-teal-600 hover:bg-teal-700">
                                <ArrowLeftEndOnRectangleIcon aria-hidden="true" className="h-6 w-6" />
                                <a href="/auth/logout" className = "px-2">Log Out</a>
                            </button> 
                        </div>
                    </div>
                </DialogPanel>
            </Dialog>
        </header>
    );
}


