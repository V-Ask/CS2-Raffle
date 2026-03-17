const WORKSHOP_REGEX_1 = /(https:\/\/)?steamcommunity\.com\/sharedfiles\/filedetails\/\?id=(\w{10})(&searchtext=.*)?/gm;
const WORKSHOP_REGEX_2 = /(https:\/\/)?steamcommunity\.com\/workshop\/filedetails\/\?id=(\w{10})(&searchtext=.*)?/gm;

function isWorkshopLink(link: string): boolean {
  return !!link.match(WORKSHOP_REGEX_1);
}

function getWorkshopId(link: string): string | null {
  let match = WORKSHOP_REGEX_1.exec(link);
  if(!match) {
    match = WORKSHOP_REGEX_2.exec(link);
  }
  return match ? match[2] : null;
}

export default {
  isWorkshopLink,
  getWorkshopId
}
