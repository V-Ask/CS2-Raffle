const WORKSHOP_REGEX = /(https:\/\/)?steamcommunity\.com\/sharedfiles\/filedetails\/\?id=(\w{10})(&searchtext=.*)?/gm;

function isWorkshopLink(link: string): boolean {
  return !!link.match(WORKSHOP_REGEX);
}

function getWorkshopId(link: string): string | null {
  let match =WORKSHOP_REGEX.exec(link);
  return match ? match[2] : null;
}

export default {
  isWorkshopLink,
  getWorkshopId
}
